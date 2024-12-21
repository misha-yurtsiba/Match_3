using System;
using UnityEngine;

public class GameOverState : GameState
{
    private readonly GameplayUIController _gameplayUIController;

    public GameOverState(GameplayUIController gameplayUIController)
    {
        _gameplayUIController = gameplayUIController;
    }

    public override void Enter()
    {
        _gameplayUIController.ActiveGameOverPanel();
        Debug.Log("LOSE");
    }

    public override void Exit()
    {
        
    }
}
