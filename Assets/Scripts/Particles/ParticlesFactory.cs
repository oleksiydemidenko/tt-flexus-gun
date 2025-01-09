using Factory;
using UnityEngine;

public class ParticlesFactory : IFactory<Particles, ParticlesType>
{
    private readonly ParticlesList _projectilesList;

    public ParticlesFactory(ParticlesList particlesList)
    {
        _projectilesList = particlesList;
    }
    
    public Particles Create(ParticlesType type)
    {
        var projectile = Object.Instantiate(_projectilesList[type].Prefab);
        return projectile;
    }
}