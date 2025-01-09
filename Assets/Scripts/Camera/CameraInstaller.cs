using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller 
{
    [SerializeField] private CameraControls _CameraControls;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_CameraControls);
    }
}