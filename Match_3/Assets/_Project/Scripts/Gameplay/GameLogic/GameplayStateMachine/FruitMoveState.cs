using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitMoveState : GameState
{
    private readonly FruitMover _fruitMover;
    private readonly MatchCheker _matchCheker;
    private readonly GameplayStateMachine _gameplayStateMachine;

    public FruitMoveState(FruitMover fruitMover, GameplayStateMachine gameplayStateMachine, MatchCheker matchCheker)
    {
        _fruitMover = fruitMover;
        _gameplayStateMachine = gameplayStateMachine;
        _matchCheker = matchCheker;
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

        fruit1.transform.DOMove(gameTile2.transform.position + new Vector3(0, 0, -0.5f),0.2f);
        fruit1.SetTile(gameTile2);
        gameTile2.curentItem = fruit1;

        fruit2.transform.DOMove(gameTile1.transform.position + new Vector3(0, 0, -0.5f), 0.2f)
            .OnComplete(() => CheckMatch());

        fruit2.SetTile(gameTile1);
        gameTile1.curentItem = fruit2;
    }

    private void CheckMatch()
    {
        IEnumerable fruits = _matchCheker.FindMatch();

        foreach(Fruit fruit in fruits)
        {
            Object.Destroy(fruit.gameObject);
        }
        _gameplayStateMachine.EnterState<PlayState>();
    }
}
