using UnityEngine;

public class GunCameraAnimation : MonoBehaviour
{
    [SerializeField] private Transform _Camera;
    [SerializeField] private AnimationCurve _RotationXCurve;
    [SerializeField] private float _Speed = 1f;
    [SerializeField] private float _PowerMultiplier = 0.025f;
    
    public bool IsPlaying { get; private set; }

    private Vector3 _eulerAngles;
    private float _power;
    private float _t;

    private void Awake()
    {
        GunEvents.ProjectileEmitted.Event += OnProjectileEmitted;
    }
    private void OnDestroy()
    {
        GunEvents.ProjectileEmitted.Event -= OnProjectileEmitted;
    }

    private void OnProjectileEmitted(GunProjectilesEmitter emitter, GunProjectile projectile)
    {
        _t = 0;
        IsPlaying = true;
        _eulerAngles = _Camera.localEulerAngles;
        _power = emitter.TrajectoryPower * _PowerMultiplier;
    }

    private void Update() 
    {
        if (!IsPlaying) return;
        
        if (_t < 1f) _t += _Speed * Time.deltaTime;
        if (_t > 1f)
        {
            _t = 1;
            IsPlaying = false;
        }
        _eulerAngles.x = _RotationXCurve.Evaluate(_t) * _power;
        _Camera.localEulerAngles = _eulerAngles;
    }
}