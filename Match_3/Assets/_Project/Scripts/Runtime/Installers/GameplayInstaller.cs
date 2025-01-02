using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private GameplayStateMachine _gameplayStateMachine;
    [SerializeField] private GameTile _gameTilePrefab;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private TimerController _timerController;
    [SerializeField] private GameplayUIController _gameplayUIController;
    public override void InstallBindings()
    {
        BindGameTilePrefab();

        BindGameScore();

        BindItemDestroyer();

        BindCompleteLevel();

        BindItemProviders();

        BindObjectPools();

        BindFactory();

        BindSwipeHandler();

        BindFruitMover();

        BindFruitSpawner();

        BindBoardGenerator();

        BindGameBoard();

        BindMatchCheker();

        BindInputHandler();

        BindGameOver();

        BindTimerController();

        BindGameplayUIController();

        BindGameplayStateMachine();
    }

    private void BindGameTilePrefab()
    {
        Container
            .BindInterfacesAndSelfTo<GameTile>()
            .FromInstance(_gameTilePrefab)
            .AsSingle()
            .NonLazy();
    }

    private void BindGameScore() 
    {
        Container
            .BindInterfacesAndSelfTo<GameScore>()
            .AsSingle();
    }

    private void BindItemDestroyer()
    {
        Container
            .BindInterfacesAndSelfTo<ItemDestroyer>()
            .AsSingle();
    }

    private void BindCompleteLevel()
    {
        Container
            .BindInterfacesAndSelfTo<CompleteLevel>()
            .AsSingle();
    }
    private void BindItemProviders()
    {
        Container
            .BindInterfacesAndSelfTo<ObstecleProvider>()
            .AsSingle();
    }

    private void BindObjectPools()
    {
        SmokeExplosion smokeExplosionPrefab = Resources.Load<SmokeExplosion>("VFX/Smoke");
        BoxExplosion boxExplosionPrefab = Resources.Load<BoxExplosion>("VFX/BoxExplosion");
        BonusExplosion bonusExplosion = Resources.Load<BonusExplosion>("VFX/BonusExplosion");

        ObjectPool<SmokeExplosion> smokeExplosionPool = new(smokeExplosionPrefab, 10);
        ObjectPool<BoxExplosion> boxExplosionPool = new(boxExplosionPrefab, 10);
        ObjectPool<BonusExplosion> bonusExplosionPool = new(bonusExplosion, 1);


        Container
            .BindInterfacesAndSelfTo<ObjectPool<SmokeExplosion>>()
            .FromInstance(smokeExplosionPool)
            .AsSingle();
        
        Container
            .BindInterfacesAndSelfTo<ObjectPool<BoxExplosion>>()
            .FromInstance(boxExplosionPool)
            .AsSingle();
        
        Container
            .BindInterfacesAndSelfTo<ObjectPool<BonusExplosion>>()
            .FromInstance(bonusExplosionPool)
            .AsSingle();
    }

    private void BindFactory()
    {
        Container
            .BindInterfacesAndSelfTo<FruitFactory>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<ObstacleFactory>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<BonusFactory>()
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

    private void BindGameOver()
    {
        Container
            .BindInterfacesAndSelfTo<GameOver>()
            .AsSingle();
    }

    private void BindTimerController()
    {
        Container
            .BindInterfacesAndSelfTo<TimerController>()
            .FromInstance(_timerController)
            .AsSingle();
    }
    private void BindGameplayUIController()
    {
        Container
            .BindInterfacesAndSelfTo<GameplayUIController>()
            .FromInstance(_gameplayUIController)
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

        Container
            .BindInterfacesAndSelfTo<CompleteLevelState>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<GameOverState>()
            .AsSingle()
            .NonLazy();

        PlayState playState = Container.Resolve<PlayState>();
        FruitMoveState FruitMoveState = Container.Resolve<FruitMoveState>();
        CompleteLevelState GameOverState = Container.Resolve<CompleteLevelState>();
        GameOverState gameOverState = Container.Resolve<GameOverState>();

        _gameplayStateMachine.Init();
        _gameplayStateMachine.AddState<PlayState>(playState);
        _gameplayStateMachine.AddState<FruitMoveState>(FruitMoveState);
        _gameplayStateMachine.AddState<CompleteLevelState>(GameOverState);
        _gameplayStateMachine.AddState<GameOverState>(gameOverState);
    }
}
