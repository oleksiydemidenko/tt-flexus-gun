using UnityEngine;

public class GunTrajectory : MonoBehaviour 
{
    [SerializeField] private Transform _Barrel;
    [SerializeField] private Transform _BarrelSart;
    [SerializeField] private LineRenderer _LineRenderer;
    [field: SerializeField] public float Power { get; private set; } = 50f;

    public int TrajectoryWayPointsCount => _LineRenderer.positionCount;
    public LineRenderer LineRenderer => _LineRenderer;
    
    private Vector3 _gravityStep;

    public void SetPower(float value)
    {
        Power = value;
    }

    private void Update() 
    {
        var startPosition = _BarrelSart.position;
        var endPosition = startPosition + Power * _Barrel.forward;

        float iF = 0f;
        for (var i = 0; i < _LineRenderer.positionCount; i++)
        {
            var progress = iF++ / _LineRenderer.positionCount;
            _gravityStep.y = -Power;
            var gravity = progress * progress * _gravityStep;
            _LineRenderer.SetPosition(i, Vector3.Lerp(startPosition, endPosition, progress) + gravity);
        }
    }
}