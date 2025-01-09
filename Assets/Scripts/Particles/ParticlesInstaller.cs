using UnityEngine;
using Zenject;

public class ParticlesInstaller : MonoInstaller
{
    [SerializeField] private ParticlesList _ParticlesList;

    public override void InstallBindings()
    {
        var factory = new ParticlesFactory(_ParticlesList);
        var pool = new MonoPool<Particles, ParticlesType>(factory);
        Container.BindInstance(pool).AsSingle();
    }
}