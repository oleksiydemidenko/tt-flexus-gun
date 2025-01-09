using Factory;
using UnityEngine;

public class GunProjectilesFactory : IFactory<GunProjectile, GunProjectileType>
{
    private readonly GunProjectilesList _projectilesList;
    private readonly CubeMeshGenerator _cubeMeshGenerator;
    private readonly CubeMeshRandomizer _cubeMeshRandomizer;

    public GunProjectilesFactory(GunProjectilesList projectilesList)
    {
        _projectilesList = projectilesList;
        _cubeMeshGenerator = new CubeMeshGenerator();
        _cubeMeshRandomizer = new CubeMeshRandomizer(_cubeMeshGenerator.vertices);
    }
    
    public GunProjectile Create(GunProjectileType type)
    {
        var projectile = Object.Instantiate(_projectilesList[type].Prefab);
        projectile.SetMesh(_cubeMeshGenerator.Generate());
        projectile.SetMeshRandomizer(_cubeMeshRandomizer);
        return projectile;
    }
}