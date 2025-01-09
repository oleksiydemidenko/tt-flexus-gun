using UnityEngine;
using Zenject;

public class GunProjectilesInstaller : MonoInstaller
{
    [SerializeField] private GunProjectilesList _GunProjectilesList;

    public override void InstallBindings()
    {
        var factory = new GunProjectilesFactory(_GunProjectilesList);
        var pool = new MonoPool<GunProjectile, GunProjectileType>(factory);
        Container.BindInstance(pool).AsSingle();
    }
}