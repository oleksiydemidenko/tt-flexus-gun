using UnityEngine;

public class GunProjectilesEmitter : MonoBehaviour
{
    [SerializeField] private GunProjectilesList _GunProjectilesList;
    [SerializeField] private GunTrajectory _GunTrajectory;
    [SerializeField, Tooltip("Meters per second")] private float _ProjectileSpeed = 3f;
    [SerializeField] private float _PowerAddProjectileSpeed = 0.01f;

    public float TrajectoryPower => _GunTrajectory.Power;

    private MonoPool<GunProjectile, GunProjectileType> _projectilesPool;

    private void Awake() 
    {    
        _projectilesPool = new (new GunProjectilesFactory(_GunProjectilesList));
    }

    public void Emit(GunProjectileType type)
    {
        var power = _GunTrajectory.Power;
        if (power <= 0) return;

        var projectile = _projectilesPool.Get(type);
        projectile.RandomizeMesh();
        projectile.SetWayPointsFromLineRenderer(_GunTrajectory.LineRenderer);

        var projectileSpeed = _ProjectileSpeed / power
            + power * _PowerAddProjectileSpeed;
        projectile.StartMove(projectileSpeed);

        GunEvents.ProjectileEmitted.Invoke(this, projectile);
    }
}