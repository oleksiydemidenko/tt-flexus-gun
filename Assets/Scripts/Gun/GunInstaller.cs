using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller 
{
    [SerializeField] private GunControls _GunControls;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_GunControls).AsSingle();
    }
}