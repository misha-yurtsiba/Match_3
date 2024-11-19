using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller 
{
    [SerializeField] private GameplayStateMachine _gameplayStateMachine;
    public override void InstallBindings()
    {
        BindGameplayStateMachine();
    }

    private void BindGameplayStateMachine()
    {
        Container
            .BindInterfacesAndSelfTo<GameplayStateMachine>()
            .FromInstance(_gameplayStateMachine)
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<PlayState>()
            .AsSingle()
            .NonLazy();

        PlayState playState = Container.Resolve<PlayState>();

        _gameplayStateMachine.Init();
        _gameplayStateMachine.AddState<PlayState>(playState);
    }
}
