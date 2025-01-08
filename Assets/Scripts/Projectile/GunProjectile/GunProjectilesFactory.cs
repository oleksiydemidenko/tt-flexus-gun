using Factory;
using UnityEngine;

public class GunProjectilesFactory : IFactory<GunProjectile, GunProjectileType>
{
    private readonly GunProjectilesList _projectilesList;

    public GunProjectilesFactory(GunProjectilesList projectilesList)
    {
        _projectilesList = projectilesList;
    }
    
    public GunProjectile Create(GunProjectileType type)
    {
        var projectile = Object.Instantiate(_projectilesList[type].Prefab);
        projectile.InitializeMesh();
        return projectile;
    }
}