using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler
{
    public Fruit[] movingFruit;

    private readonly InputHandler _inputHandler;
    private readonly GameBoard _gameBoard;
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly Camera _camera;

    private GameTile _startGameTile;

    public SwipeHandler(InputHandler inputHandler, GameBoard gameBoard, GameplayStateMachine gameplayStateMachine)
    {
        _inputHandler = inputHandler;
        _gameBoard = gameBoard;
        _gameplayStateMachine = gameplayStateMachine;

        _camera = Camera.main;
        _inputHandler.startSwipe += StartSwipe;
        _inputHandler.onSwipe += EndSwipe;
        movingFruit = new Fruit[2];
    }

    public void Init()
    {

    }

    private void StartSwipe(Vector2 startPosition)
    {
        Ray ray = _camera.ScreenPointToRay(startPosition);
        RaycastHit raycastHit;

        if(Physics.Raycast(ray,out raycastHit, 100) && raycastHit.transform.TryGetComponent(out GameTile gameTile))
        {
            Fruit fruit = gameTile.curentItem as Fruit;

            _startGameTile = (fruit != null) ? gameTile : null;
        }
    }

    private void EndSwipe(Vector2Int swipeDirection)
    {
        if(_startGameTile == null) return;

        if(swipeDirection == Vector2Int.up)
        {
            if(_startGameTile.yPos < _gameBoard.y - 1 && _gameBoard.GetTile(_startGameTile.xPos,_startGameTile.yPos + 1).curentItem is Fruit)
                ChoseMovingFruit(Vector2Int.up);
        }
        else if(swipeDirection == Vector2Int.down)
        {
            if(_startGameTile.yPos > 0 && _gameBoard.GetTile(_startGameTile.xPos, _startGameTile.yPos - 1).curentItem is Fruit)
                ChoseMovingFruit(Vector2Int.down);
        }
        else if(swipeDirection == Vector2Int.left)
        {
            if(_startGameTile.xPos > 0 && _gameBoard.GetTile(_startGameTile.xPos - 1, _startGameTile.yPos).curentItem is Fruit)
                ChoseMovingFruit(Vector2Int.left);
        }
        else if(swipeDirection == Vector2Int.right)
        {
            if (_startGameTile.xPos < _gameBoard.x - 1 && _gameBoard.GetTile(_startGameTile.xPos + 1, _startGameTile.yPos).curentItem is Fruit)
                ChoseMovingFruit(Vector2Int.right);
        }
    }

    private void ChoseMovingFruit(Vector2Int direction)
    {
        movingFruit[0] = (Fruit)_startGameTile.curentItem;
        movingFruit[1] = (Fruit)_gameBoard.GetTile(_startGameTile.xPos + direction.x, _startGameTile.yPos + direction.y).curentItem;
        _gameplayStateMachine.EnterState<FruitMoveState>();
    }
}
