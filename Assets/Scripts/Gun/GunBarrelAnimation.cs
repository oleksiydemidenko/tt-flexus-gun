using UnityEngine;

public class GunBarrelAnimation : MonoBehaviour
{
    [SerializeField] private Transform _Barrel;
    [SerializeField] private AnimationCurve _PositionZCurve;
    [SerializeField] private float _PowerMultiplier = 0.025f;
    [SerializeField] private float _Speed = 1f;
    
    public bool IsPlaying { get; private set; }

    private Vector3 _position;
    private float _t;
    private float _power;

    private void Update() 
    {
        if (!IsPlaying) return;
        
        if (_t < 1f) _t += _Speed * Time.deltaTime;
        if (_t > 1f)
        {
            _t = 1;
            IsPlaying = false;
        }
        _position.z = _PositionZCurve.Evaluate(_t) * _power;
        _Barrel.localPosition = _position;
    }
    public void Play(float power)
    {
        _t = 0;
        IsPlaying = true;
        _Barrel.localPosition = _position = Vector3.zero;
        _power = power * _PowerMultiplier;
    }
}