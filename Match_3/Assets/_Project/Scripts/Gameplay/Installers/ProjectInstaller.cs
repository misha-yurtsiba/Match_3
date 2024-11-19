using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private LoadingCurtain _loadingCurtain;

    public void Initialize()
    {
        

    }

    public override void InstallBindings()
    {
        BindLoadingCurtain();

        BindStateMachine();

        BindSceneLoader();

        BindGameStates();


    }
    private void BindLoadingCurtain()
    {
        Container.
            BindInterfacesAndSelfTo<LoadingCurtain>()
            .FromInstance(_loadingCurtain);
    }
    private void BindStateMachine()
    {
        Container
            .BindInterfacesAndSelfTo<StateMachine>()
            .AsSingle()
            .NonLazy();
    }

    private void BindGameStates()
    {
        Container.BindInterfacesAndSelfTo<LoadingSceneState>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameplayState>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<MenuState>()
            .AsSingle()
            .NonLazy();

        StateMachine stateMachine = Container.Resolve<StateMachine>();

        stateMachine.AddState<MenuState>(Container.Resolve<MenuState>());
        stateMachine.AddState<LoadingSceneState>(Container.Resolve<LoadingSceneState>());
        stateMachine.AddState<GameplayState>(Container.Resolve<GameplayState>());
    }

    private void BindSceneLoader()
    {
        Container
            .BindInterfacesAndSelfTo<SceneChanger>()
            .AsSingle();
    }

}
