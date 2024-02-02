using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private CameraModeSwitcher _cameraModeSwitcher;

    public override void InstallBindings()
    {
        BindInput();
        BindCameraSwitcher();

    }

    private void BindCameraSwitcher()
    {
        //Container.Bind<CameraModeSwitcher>().FromInstance(_cameraModeSwitcher).AsSingle();
    }

    private void BindInput()
    {
        CharacterInput input = new CharacterInput();
        input.Enable();

        Container.Bind<CharacterInput>().FromInstance(input).AsSingle();
    }
}