using UnityEngine;

public class GunProjectile : MonoBehaviour, IPoolInstance<GunProjectile, GunProjectileType>
{
    [SerializeField] private MeshFilter _MeshFilter;
    [SerializeField] private float _Speed = 1;
    [SerializeField] private float _SmoothSpeed = 24;
    [SerializeField] private LayerMask _RayMask;
    [SerializeField] private ParticlesType _DetonateParticlesType;
    [SerializeField] private float[] _Ricochets;

    public GunProjectileType Type { get; set; }
    public GunProjectile MonoInstance { get; set; }
    public bool Free { get; set; }
    public bool IsMoving { get; private set; }

    private Vector3 _lastPosition;
    private Vector3[] _waypoints = new Vector3[0];
    private float _moveT;
    private MonoPool<Particles, ParticlesType> _particlesPool;
    private Particles _particles;
    private int _currentRicochet;
    private int _currentVertIndex;
    private IMeshRandomizer _meshRandomizer;

    public void ReusePoolInstance()
    {
        gameObject.SetActive(true);
        Free = false;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        Free = true;
    }

    private void Update() 
    {
        WaypointsMovement();
    }

    private void WaypointsMovement()
    {
        if (!IsMoving) return;

        _lastPosition = transform.position;
        var dT = Time.deltaTime;
        
        if (_moveT > 1f)
        {
            Detonate();
            IsMoving = false;
        }
        else
        {
            _moveT += dT * _Speed;
            _currentVertIndex = (int)(_moveT * _waypoints.Length);
            if (_waypoints.Length > 1)
            {
                if (_currentVertIndex >= _waypoints.Length) _currentVertIndex = _waypoints.Length - 1;
                transform.position -= dT * _SmoothSpeed 
                    * (transform.position - _waypoints[_currentVertIndex]);
            }
        }

        CheckHit();
    }

    private void CheckHit()
    {
        var velocity = transform.position - _lastPosition;
        var rayDirection = velocity.normalized 
            * Vector3.Distance(_lastPosition, _waypoints[_currentVertIndex]);

        Debug.DrawRay(_lastPosition, rayDirection, Color.red);
        Debug.DrawRay(_lastPosition + rayDirection, rayDirection * 0.1f, Color.green);

        if (Physics.Linecast(_lastPosition, _waypoints[_currentVertIndex], 
            out var hit, _RayMask))
        {
            GunProjectileEvents.Hit.Invoke(this, hit);
            if (_currentRicochet == _Ricochets.Length)
            {
                Detonate();
                _particles.transform.SetPositionAndRotation(hit.point,
                    Quaternion.LookRotation(hit.normal, Vector3.up));
            }
            else CalculateRicochet(velocity, hit);
        }
    }

    private void CalculateRicochet(Vector3 velocity, RaycastHit hit)
    {
        var reflectVelocity = Vector3.Reflect(velocity, hit.normal) 
            * _Ricochets[_currentRicochet];
        var startPosition = hit.point;
        var endPosition = startPosition + reflectVelocity;

        float iF = 0f;
        var gravityStep = Vector3.zero;
        var count = _waypoints.Length;
        for (var i = 0; i < count; i++)
        {
            var progress = iF++ / count;
            gravityStep.y = -reflectVelocity.magnitude;
            var gravity = progress * progress * gravityStep;
            _waypoints[i] = Vector3.Lerp(startPosition, endPosition, progress) + gravity;
        }

        _moveT = 0;
        _currentRicochet++;
        transform.position = startPosition;

        _meshRandomizer.Randomize(_MeshFilter.mesh);
    }

    private void Detonate()
    {
        Deactivate();
        _particles = _particlesPool.Get(_DetonateParticlesType);
        _particles.transform.position = transform.position;
        _particles.Play();
        _currentRicochet = 0;
        IsMoving = false;
    }

    public void SetParticlesPool(MonoPool<Particles, ParticlesType> particlesPool)
        => _particlesPool = particlesPool;
    public void SetWaypointsFromLineRenderer(LineRenderer lineRenderer)
    {
        var pointsCount = lineRenderer.positionCount;
        if (_waypoints.Length != pointsCount) 
            _waypoints = new Vector3[pointsCount];

        lineRenderer.GetPositions(_waypoints);
    }
    public void StartMoveWaypoints(float speed)
    {
        _moveT = 0;
        transform.position = _waypoints[0];
        IsMoving = true;
        _Speed = speed;
        _meshRandomizer.Randomize(_MeshFilter.mesh);
    }

    public void SetMesh(Mesh mesh)
    {
        _MeshFilter.mesh = mesh;
    }
    public void SetMeshRandomizer(IMeshRandomizer meshRandomizer)
    {
        _meshRandomizer = meshRandomizer;
    }
}