using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FruitMoveState : GameState 
{
    private readonly SwipeHandler _swipeHandler;
    private readonly FruitMover _fruitMover;
    private readonly MatchCheker _matchCheker;
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameBoard _gameBoard;
    private readonly FruitSpawner _fruitSpawner;
    private readonly ItemDestroyer _itemDestroyer;
    private readonly CompleteLevel _completeLevel;
    private readonly GameOver _gameOver;

    private Dictionary<IMoveble, GameTile> _movedFruits = new Dictionary<IMoveble, GameTile>();
    private List<UniTask> _destroyedFruits = new List<UniTask>();

    public CancellationTokenSource cts;
    public FruitMoveState(
        SwipeHandler swipeHandler,
        GameplayStateMachine gameplayStateMachine,
        MatchCheker matchCheker,
        GameBoard gameBoard,
        FruitMover fruitMover,
        FruitSpawner fruitSpawner,
        ItemDestroyer itemDestroyer,
        CompleteLevel completeLevel,
        GameOver gameOver
        )
    {
        _swipeHandler = swipeHandler;
        _gameplayStateMachine = gameplayStateMachine;
        _matchCheker = matchCheker;
        _gameBoard = gameBoard;
        _fruitMover = fruitMover;
        _fruitSpawner = fruitSpawner;
        _itemDestroyer = itemDestroyer;
        _completeLevel = completeLevel;
        _gameOver = gameOver;

        cts = new CancellationTokenSource();

        Application.quitting += () =>
        {
            Debug.Log("Cancel");
            cts.Cancel();
            cts.Dispose();
        };
    }

    public async override void Enter()
    {
        try
        {
            await _fruitMover.SwapFruitsAsync(_swipeHandler.movingFruit[0], _swipeHandler.movingFruit[1], cancellationToken: cts.Token).AttachExternalCancellation(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Cancel");
        }

        CheckMatch();
    }

    public override void Exit()
    {
        Debug.Log("Exit");

        cts.Cancel();
        cts.Dispose();
        cts = new CancellationTokenSource();
    }

    private async void CheckMatch()
    {
        _movedFruits.Clear();
        _destroyedFruits.Clear();

        List<Fruit> fruits = _matchCheker.FindMatch();

        if (_gameOver.IsGameOver)
        {
            _gameplayStateMachine.EnterState<GameOverState>();
            return;
        }

        if (fruits.Count == 0)
        {
            _gameplayStateMachine.EnterState<PlayState>();
        }
        else
        {
            foreach (Fruit fruit in fruits)
            {
                fruit.DestroyAction(_gameBoard);
                _itemDestroyer.DestroyOneItem(fruit);
                
                await UniTask.DelayFrame(2);
            }

            try
            {
                await _itemDestroyer.DestroyItemsAsync(cts.Token).AttachExternalCancellation(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Cancel");
            }

            RestoreBoard().Forget();
        }
    }

    private async UniTaskVoid RestoreBoard()
    {
        FindMovingItems();

        await _fruitMover.MoveFruitDown(_movedFruits, 5f, cts.Token).AttachExternalCancellation(cts.Token);
        await _fruitSpawner.SpawnDeletedFruits(cts.Token).AttachExternalCancellation(cts.Token);

        if (_completeLevel.IsLevelComplet())
            _gameplayStateMachine.EnterState<CompleteLevelState>();
        else
            CheckMatch();
    }

    private void FindMovingItems()
    {
        int nullCount = 0;

        for (int i = 0; i < _gameBoard.x; i++)
        {
            for (int j = 0; j < _gameBoard.y; j++)
            {
                if (_gameBoard.GetTile(i, j).curentItem == null)
                {
                    nullCount++;
                }
                else
                {
                    if (nullCount > 0 && _gameBoard.GetTile(i, j).curentItem is IMoveble moveble)
                    {
                        SelectMovingFruit(moveble, i, j - nullCount);
                    }
                    else
                    {
                        nullCount = 0;
                    }
                }
            }
            nullCount = 0;
        }
    }

    private void SelectMovingFruit(IMoveble moveble, int x, int y)
    {
        if(moveble is Item item)
        {
            GameTile tile = _gameBoard.GetTile(x, y);
            item.CurentTile.curentItem = null;
            tile.curentItem = item;
            item.SetTile(tile);

            _movedFruits.Add(moveble, tile);
        }
    }
}