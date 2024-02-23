using UnityEngine;
using UnityEngine.VFX;
using Zenject;

public class CaptureSystemInstaller : MonoInstaller
{
    [SerializeField] private Hat _hat;
    [SerializeField] private SplineCaptureBenderAnimation _captureBenderAnimation;
    [SerializeField] private VisualEffect _dissolveEffect;

    public override void InstallBindings()
    {
        BindStateMachine();
    }

    private void BindStateMachine()
    {
        Container.BindInterfacesAndSelfTo<HatThrowerStateMachine>().AsSingle();

        Container.Bind<ThrowerStateMachineData>().AsSingle();

        Container.Bind<VisualEffect>().FromInstance(_dissolveEffect).AsSingle()
            .WhenInjectedInto<CapturedAnimationTransition>();

        Container.Bind<Hat>().FromInstance(_hat);

        Container.Bind<SplineCaptureBenderAnimation>().FromInstance(_captureBenderAnimation).AsSingle();

        BindTransitions();

        Container.Bind<HatSetter>().AsSingle();

        Container.BindInterfacesAndSelfTo<HatOnThrowerHeadState>().AsSingle();
        Container.BindInterfacesAndSelfTo<ThrowingState>().AsSingle();
        Container.BindInterfacesAndSelfTo<CapturedState>().AsSingle();

        Container.Bind<HatThrowerStatesProvider>().AsSingle().NonLazy();
    }

    private void BindTransitions()
    {
        Container.BindInterfacesAndSelfTo<UncapturedAnimationTransition>().AsSingle();
        Container.BindInterfacesAndSelfTo<CapturedAnimationTransition>().AsSingle();
    }
}
