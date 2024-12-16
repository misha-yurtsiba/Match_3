using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    public bool IsGameOver { get; private set; } = false;
    public GameOver(GameplayStateMachine gameplayStateMachine)
    {
        _gameplayStateMachine = gameplayStateMachine;
    }

    public void LoseGame()
    {
        if(_gameplayStateMachine.GetCurentStateType() == typeof(FruitMoveState))
            IsGameOver = true;
        else
            _gameplayStateMachine.EnterState<GameOverState>();    
    }
}
