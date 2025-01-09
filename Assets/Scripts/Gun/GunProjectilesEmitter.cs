using UnityEngine;
using Zenject;

public class GunProjectilesEmitter : MonoBehaviour
{
    [SerializeField] private GunTrajectory _GunTrajectory;
    [SerializeField, Tooltip("Meters per second")] private float _ProjectileSpeed = 3f;
    [SerializeField] private float _PowerAddProjectileSpeed = 0.01f;
    private MonoPool<GunProjectile, GunProjectileType> _projectilesPool;
    private MonoPool<Particles, ParticlesType> _particlesPool;

    public float TrajectoryPower => _GunTrajectory.Power;

    [Inject]
    private void Construct(MonoPool<GunProjectile, GunProjectileType> projectilesPool,
        MonoPool<Particles, ParticlesType> particlesPool)
    {
        _projectilesPool = projectilesPool;
        _particlesPool = particlesPool;
    }

    public void Emit(GunProjectileType type)
    {
        var power = _GunTrajectory.Power;
        if (power <= 0) return;

        var projectile = _projectilesPool.Get(type);
        projectile.SetWaypointsFromLineRenderer(_GunTrajectory.LineRenderer);
        projectile.SetParticlesPool(_particlesPool);

        var projectileSpeed = _ProjectileSpeed / power
            + power * _PowerAddProjectileSpeed;
        projectile.StartMoveWaypoints(projectileSpeed);

        GunEvents.ProjectileEmitted.Invoke(this, projectile);
    }
}