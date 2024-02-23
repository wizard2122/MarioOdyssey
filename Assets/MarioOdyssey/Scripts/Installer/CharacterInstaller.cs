using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private CharacterView _characterView;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Character>().FromInstance(_character).AsSingle();
        Container.BindInterfacesAndSelfTo<CharacterMover>().FromInstance(_characterMover).AsSingle();
        Container.Bind<CharacterView>().FromInstance(_characterView).AsSingle();
    }
}
