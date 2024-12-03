using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class FruitMoveState : GameState 
{
    private readonly SwipeHandler _swipeHandler;
    private readonly FruitMover _fruitMover;
    private readonly MatchCheker _matchCheker;
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameBoard _gameBoard;
    private readonly FruitSpawner _fruitSpawner;

    private Dictionary<Fruit, GameTile> _movedFruits = new Dictionary<Fruit, GameTile>();
    private List<UniTask> _destroyedFruits = new List<UniTask>();

    private CancellationTokenSource cts;
    public FruitMoveState(
        SwipeHandler swipeHandler,
        GameplayStateMachine gameplayStateMachine,
        MatchCheker matchCheker,
        GameBoard gameBoard,
        FruitMover fruitMover,
        FruitSpawner fruitSpawner)
    {
        _swipeHandler = swipeHandler;
        _gameplayStateMachine = gameplayStateMachine;
        _matchCheker = matchCheker;
        _gameBoard = gameBoard;
        _fruitMover = fruitMover;
        _fruitSpawner = fruitSpawner;

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
        await _fruitMover.SwapFruitsAsync(_swipeHandler.movingFruit[0], _swipeHandler.movingFruit[1],cts.Token);
        CheckMatch();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        
    }

    private async void CheckMatch()
    {
        _movedFruits.Clear();
        _destroyedFruits.Clear();

        List<Fruit> fruits = _matchCheker.FindMatch();

        if(fruits.Count == 0)
        {
            _gameplayStateMachine.EnterState<PlayState>();
        }
        else
        {
            foreach (Fruit fruit in fruits)
            {
                fruit.CurentTile.curentItem = null;
                _destroyedFruits.Add(DestroyFruitAsync(fruit));

                await UniTask.DelayFrame(2);
            }

            await UniTask.WhenAll(_destroyedFruits);

            RestoreBoard();
        }
    }

    private async UniTask DestroyFruitAsync(Fruit fruit)
    {
        Tween tween = fruit.transform.DOScale(Vector3.zero, 0.2f);

        await tween.AsyncWaitForCompletion();

        UnityEngine.Object.Destroy(fruit.gameObject);
    }

    private async UniTaskVoid RestoreBoard()
    {
        int nullCount = 0;

        for (int i = 0; i < _gameBoard.x; i++)
        {
            for (int j = 0; j < _gameBoard.y; j++)
            {
                if(_gameBoard.GetTile(i,j).curentItem == null)
                {
                    nullCount++;
                }
                else
                {
                    if(nullCount > 0)
                    {
                        SelectMovingFruit(_gameBoard.GetTile(i, j).curentItem as Fruit, i, j - nullCount);
                    }
                }
            }
            nullCount = 0;
        }

        await _fruitMover.MoveFruitDown(_movedFruits, 0.7f,cts.Token);
        await _fruitSpawner.SpawnDeletedFruits(cts.Token);

        CheckMatch();
    }

    private void SelectMovingFruit(Fruit fruit, int x, int y)
    {
        GameTile tile = _gameBoard.GetTile(x, y);
        fruit.CurentTile.curentItem = null;
        tile.curentItem = fruit;
        fruit.SetTile(tile);
        _movedFruits.Add(fruit, tile);
    }
}
