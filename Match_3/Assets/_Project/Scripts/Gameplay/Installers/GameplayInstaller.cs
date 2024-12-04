using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller 
{
    [SerializeField] private GameplayStateMachine _gameplayStateMachine;
    [SerializeField] private GameTile _gameTilePrefab;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private FruitsPrefabs _fruitsPrefabs;
    public override void InstallBindings()
    {
        BindGameTilePrefab();

        BindFactory();

        BindSwipeHandler();

        BindFruitMover();

        BindFruitSpawner();

        BindBoardGenerator();

        BindGameBoard();

        BindMatchCheker();

        BindInputHandler();

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

        Container
            .BindInterfacesAndSelfTo<ObstacleFactory>()
            .AsSingle();
    }

    private void BindSwipeHandler()
    {
        Container
            .BindInterfacesAndSelfTo<SwipeHandler>()
            .AsSingle();
    }

    private void BindFruitMover()
    {
        Container
            .BindInterfacesAndSelfTo<FruitMover>()
            .AsSingle();
    }

    private void BindFruitSpawner()
    {
        Container
            .BindInterfacesAndSelfTo<FruitSpawner>()
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

    private void BindMatchCheker()
    {
        Container
            .BindInterfacesAndSelfTo<MatchCheker>()
            .AsSingle();
    }
    private void BindInputHandler()
    {
        Container
            .BindInterfacesAndSelfTo<InputHandler>()
            .FromInstance(_inputHandler)
            .AsSingle();
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

        Container
            .BindInterfacesAndSelfTo<FruitMoveState>()
            .AsSingle()
            .NonLazy();

        PlayState playState = Container.Resolve<PlayState>();
        FruitMoveState FruitMoveState = Container.Resolve<FruitMoveState>();

        _gameplayStateMachine.Init();
        _gameplayStateMachine.AddState<PlayState>(playState);
        _gameplayStateMachine.AddState<FruitMoveState>(FruitMoveState);
    }
}
