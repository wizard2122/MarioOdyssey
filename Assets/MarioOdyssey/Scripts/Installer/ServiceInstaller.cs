using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _coroutinePerformer;
    public override void InstallBindings()
    {
        BindInput();
        BindPauseHandler();
        BindCameraSwitcher();

        Container.BindInterfacesAndSelfTo<ICoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle();
    }

    private void BindPauseHandler()
    {
        Container.Bind<PauseHandler>().AsSingle();
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