using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitMoveState : GameState
{
    private readonly FruitMover _fruitMover;
    private readonly MatchCheker _matchCheker;
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly GameBoard _gameBoard;

    private Dictionary<Fruit, GameTile> _movedFruits = new Dictionary<Fruit, GameTile>();

    private float _moveSpeed = 10;
    private bool _isMoving = false;

    public FruitMoveState(FruitMover fruitMover, GameplayStateMachine gameplayStateMachine, MatchCheker matchCheker, GameBoard gameBoard)
    {
        _fruitMover = fruitMover;
        _gameplayStateMachine = gameplayStateMachine;
        _matchCheker = matchCheker;
        _gameBoard = gameBoard;
    }

    public override void Enter()
    {
        MoveFruits(_fruitMover.movingFruit[0], _fruitMover.movingFruit[1]);
    }

    public override void Update()
    {
        if (!_isMoving) return;

        if(_movedFruits.Count == 0)
        {
            _isMoving = false;
            CheckMatch();
            return;
        }

        foreach (KeyValuePair<Fruit, GameTile> pair in _movedFruits)
        {
            Fruit fruit = pair.Key;
            GameTile tile = pair.Value;

            if(fruit.transform.position == tile.transform.position + _gameBoard.itemOffset)
            {
                _movedFruits.Clear();
                break;
            }
            else
            {
                fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, tile.transform.position + _gameBoard.itemOffset, _moveSpeed * Time.deltaTime);
            }
        }
    }

    public override void Exit()
    {
        
    }

    private void MoveFruits(Fruit fruit1, Fruit fruit2)
    {
        GameTile gameTile1 = fruit1.CurentTile;
        GameTile gameTile2 = fruit2.CurentTile;

        fruit1.transform.DOMove(gameTile2.transform.position + _gameBoard.itemOffset,0.2f);
        fruit1.SetTile(gameTile2);
        gameTile2.curentItem = fruit1;

        fruit2.transform.DOMove(gameTile1.transform.position + _gameBoard.itemOffset, 0.2f)
            .OnComplete(() => CheckMatch());

        fruit2.SetTile(gameTile1);
        gameTile1.curentItem = fruit2;
    }

    private async void CheckMatch()
    {
        _movedFruits.Clear();
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
                fruit.transform
                    .DOScale(Vector3.zero, 0.2f)
                    .OnComplete(() => Object.Destroy(fruit.gameObject));
            }

            await Task.Delay(200);
            RestoreBoard();
        }
    }

    private void RestoreBoard()
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
                        MoveOneFruit(_gameBoard.GetTile(i, j).curentItem as Fruit, i, j - nullCount);
                }
            }
            nullCount = 0;
        }
        _isMoving = true;
    }

    private void MoveOneFruit(Fruit fruit, int x, int y)
    {
        GameTile tile = _gameBoard.GetTile(x, y);
        fruit.CurentTile.curentItem = null;
        tile.curentItem = fruit;
        fruit.SetTile(tile);
        _movedFruits.Add(fruit, tile);
    }
}
