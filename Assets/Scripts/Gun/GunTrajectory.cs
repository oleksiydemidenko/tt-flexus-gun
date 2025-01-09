using UnityEngine;

public class GunTrajectory : MonoBehaviour 
{
    [SerializeField] private Transform _Barrel;
    [SerializeField] private Transform _BarrelSart;
    [SerializeField] private LineRenderer _LineRenderer;
    [field: SerializeField] public float Power { get; private set; } = 50f;

    public int TrajectoryWayPointsCount => _LineRenderer.positionCount;
    public LineRenderer LineRenderer => _LineRenderer;
    

    public void SetPower(float value)
    {
        Power = value;
    }

    private void Update() 
    {
        var startPosition = _BarrelSart.position;
        var endPosition = startPosition + Power * _Barrel.forward;

        var iF = 0f;
        var gravityStep = Vector3.zero;
        for (var i = 0; i < _LineRenderer.positionCount; i++)
        {
            var progress = iF++ / _LineRenderer.positionCount;
            gravityStep.y = -Power;
            var gravity = progress * progress * gravityStep;
            _LineRenderer.SetPosition(i, Vector3.Lerp(startPosition, endPosition, progress) + gravity);
        }
    }
}