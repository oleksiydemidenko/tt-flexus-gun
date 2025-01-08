using System;
using UnityEngine;

public class GunProjectile : MonoBehaviour, IPoolInstance<GunProjectile, GunProjectileType>
{
    [SerializeField] private MeshFilter _MeshFilter;
    [SerializeField] private float _Speed = 1;
    [SerializeField] private float _SmoothSpeed = 24;
    [SerializeField] private LayerMask _RayMask;

    public GunProjectileType Type { get; set; }
    public GunProjectile MonoInstance { get; set; }
    public bool Free { get; set; }
    public bool IsMoving { get; private set; }

    private Vector3 _lastPosition;
    private Mesh _cubeMesh;
    private Vector3[] _vertices = new Vector3[0];
    private Vector3[] _wayPoints = new Vector3[0];
    private float _moveT;

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
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!IsMoving) return;

        var dT = Time.deltaTime;
        _moveT += dT * _Speed;
        var vertIndex = (int)(_moveT * _wayPoints.Length);
        if (_wayPoints.Length > 1)
        {
            if (vertIndex >= _wayPoints.Length) vertIndex = _wayPoints.Length - 1;
            transform.position -= dT * _SmoothSpeed 
                * (transform.position - _wayPoints[vertIndex]);
        }
        
        if (_moveT > 1f)
        {
            Detonate();
            IsMoving = false;
        }
    }

    private void LateUpdate()
    {
        _lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Detonate();
    }

    private void Detonate()
    {
        var velocity = transform.position - _lastPosition;
        if (Physics.Raycast(_lastPosition, velocity.normalized, 
            out var hit, 10, _RayMask))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        Deactivate();
    }

    public void SetWayPointsFromLineRenderer(LineRenderer lineRenderer)
    {
        var pointsCount = lineRenderer.positionCount;
        if (_wayPoints.Length != pointsCount) 
            _wayPoints = new Vector3[pointsCount];

        lineRenderer.GetPositions(_wayPoints);
    }
    public void StartMove(float speed)
    {
        _moveT = 0;
        transform.position = _wayPoints[0];
        IsMoving = true;
        _Speed = speed;
    }
    public void InitializeMesh()
    {
        _cubeMesh = new Mesh();
        var vertices = new Vector3[] 
        {
            //F
            new (-1, -1,  1),
            new ( 1, -1,  1),
            new ( 1,  1,  1),
            new (-1,  1,  1),

            //B
            new (-1, -1, -1),
            new ( 1, -1, -1),
            new ( 1,  1, -1),
            new (-1,  1, -1),

            //U
            new (-1,  1,  1),
            new ( 1,  1,  1),
            new ( 1,  1, -1),
            new (-1,  1, -1),

            //D
            new (-1, -1,  1),
            new ( 1, -1,  1),
            new ( 1, -1, -1),
            new (-1, -1, -1),

            //R
            new ( 1, -1,  1),
            new ( 1,  1,  1),
            new ( 1,  1, -1),
            new ( 1, -1, -1),

            //L
            new (-1, -1,  1),
            new (-1,  1,  1),
            new (-1,  1, -1),
            new (-1, -1, -1)
        };
        
        _vertices = new Vector3[vertices.Length];
        Array.Copy(vertices, _vertices, vertices.Length);

        var triangles = new int[] 
        {
            0, 1, 2,
            2, 3, 0,

            7, 5, 4,
            6, 5, 7,

            8, 9,  10,
            8,  10,  11,

            12,  14,  13,
            12,  15,  14,

            16,  18,  17,
            16,  19,  18,

            20, 21, 22,
            20, 22, 23
        };

        var uv = new Vector2[] 
        {
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.right, Vector2.zero, Vector2.up, Vector2.one,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up
        };

        _cubeMesh.vertices = vertices;
        _cubeMesh.triangles = triangles;
        _cubeMesh.uv = uv;
        _cubeMesh.RecalculateNormals();
        _cubeMesh.RecalculateBounds();
        _MeshFilter.mesh = _cubeMesh;
    }
    public void RandomizeMesh()
    {
        var verts = _cubeMesh.vertices;
        var rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[0] = _vertices[0] + rand;
        verts[12] = _vertices[12] + rand;
        verts[20] = _vertices[20] + rand;

        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[1] = _vertices[1] + rand;
        verts[13] = _vertices[13] + rand;
        verts[16] = _vertices[16] + rand;
        
        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[4] = _vertices[4] + rand;
        verts[15] = _vertices[15] + rand;
        verts[23] = _vertices[23] + rand;
        
        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[5] = _vertices[5] + rand;
        verts[14] = _vertices[14] + rand;
        verts[19] = _vertices[19] + rand;
        
        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[7] = _vertices[7] + rand;
        verts[11] = _vertices[11] + rand;
        verts[22] = _vertices[22] + rand;
        
        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[6] = _vertices[6] + rand;
        verts[10] = _vertices[10] + rand;
        verts[18] = _vertices[18] + rand;
        
        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[3] = _vertices[3] + rand;
        verts[8] = _vertices[8] + rand;
        verts[21] = _vertices[21] + rand;
        
        rand = UnityEngine.Random.insideUnitSphere * 0.25f;
        verts[2] = _vertices[2] + rand;
        verts[9] = _vertices[9] + rand;
        verts[17] = _vertices[17] + rand;

        _cubeMesh.vertices = verts;
        _cubeMesh.RecalculateNormals();
        _cubeMesh.RecalculateBounds();
    }
}