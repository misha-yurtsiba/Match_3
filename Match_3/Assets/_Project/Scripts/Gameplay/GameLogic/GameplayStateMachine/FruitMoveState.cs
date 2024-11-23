using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMoveState : GameState
{
    private readonly FruitMover _fruitMover;
    private readonly GameplayStateMachine _gameplayStateMachine;

    public FruitMoveState(FruitMover fruitMover, GameplayStateMachine gameplayStateMachine)
    {
        _fruitMover = fruitMover;
        _gameplayStateMachine = gameplayStateMachine;
    }

    public override void Enter()
    {
        MoveFruits(_fruitMover.movingFruit[0], _fruitMover.movingFruit[1]);
    }

    public override void Exit()
    {
        
    }

    private void MoveFruits(Fruit fruit1, Fruit fruit2)
    {
        GameTile gameTile1 = fruit1.CurentTile;
        GameTile gameTile2 = fruit2.CurentTile;

        fruit1.transform.position = gameTile2.transform.position;
        fruit1.SetTile(gameTile2);
        gameTile2.curentItem = fruit1;

        fruit2.transform.position = gameTile1.transform.position;
        fruit2.SetTile(gameTile1);
        gameTile1.curentItem = fruit2;

        _gameplayStateMachine.EnterState<PlayState>();
    }
}
