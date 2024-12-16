using System;
using UnityEngine;

public class GameOverState : GameState
{
    public override void Enter()
    {
        Debug.Log("LOSE");
    }

    public override void Exit()
    {
        
    }
}
