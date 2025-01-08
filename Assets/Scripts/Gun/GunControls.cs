using UnityEngine;

public class GunControls : MonoBehaviour
{
    [SerializeField] private GunBarrelAnimation _BarrelAnimation;
    [SerializeField] private Transform _Barrel;
    [SerializeField] private float _BarrelRotateSpeed = 100f;

    [Space]
    [SerializeField] private GunTrajectory _GunTrajectory;
    [SerializeField] private float _TrajectoryPowerMultiplier = 100f;

    [Space]
    [SerializeField] private GunProjectilesEmitter _GunProjectilesEmitter;
    [SerializeField] private GunProjectileType _GunProjectileType;
    
    public void SetPower(float value) => _GunTrajectory.SetPower(value * _TrajectoryPowerMultiplier);
    public void RotateVertival(float value)
    {
        _Barrel.rotation *= Quaternion.Euler(_BarrelRotateSpeed * value * Time.deltaTime, 0, 0);
    }
    public void EmitProjectile()
    {
        _GunProjectilesEmitter.Emit(_GunProjectileType);
        _BarrelAnimation.Play(_GunProjectilesEmitter.TrajectoryPower);
    }
}
