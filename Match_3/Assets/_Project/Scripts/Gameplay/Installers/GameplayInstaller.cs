using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller 
{
    [SerializeField] private GameplayStateMachine _gameplayStateMachine;
    [SerializeField] private GameTile _gameTilePrefab;
    [SerializeField] private FruitsPrefabs _fruitsPrefabs;
    public override void InstallBindings()
    {
        BindGameTilePrefab();

        BindFactory();

        BindBoardGenerator();

        BindGameBoard();

        BindGameplayStateMachine();
    }

    private void BindGameTilePrefab()
    {
        Container
            .BindInterfacesAndSelfTo<GameTile>()
            .FromInstance(_gameTilePrefab)
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<FruitsPrefabs>()
            .FromInstance(_fruitsPrefabs)
            .AsSingle()
            .NonLazy();
    }
    private void BindFactory()
    {
        Container
            .BindInterfacesAndSelfTo<FruitFactory>()
            .AsSingle();
    }

    private void BindBoardGenerator()
    {
        Container
            .BindInterfacesAndSelfTo<BoardGenerator>()
            .AsSingle();
    }

    private void BindGameBoard()
    {
        Container
            .BindInterfacesAndSelfTo<GameBoard>()
            .AsSingle()
            .NonLazy();
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
