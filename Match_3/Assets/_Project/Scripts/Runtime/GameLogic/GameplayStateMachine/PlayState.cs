using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState
{
    private InputHandler _inputHandler;

    public PlayState(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    public override void Enter()
    {
        Debug.Log("Play");
        _inputHandler.EnableInput();
    }

    public override void Exit()
    {
        _inputHandler.DisableInput();
    }
}
