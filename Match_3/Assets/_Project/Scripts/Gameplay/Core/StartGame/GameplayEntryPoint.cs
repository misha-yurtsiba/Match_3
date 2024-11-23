using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    private GameplayStateMachine _gameplayStateMachine;
    private GameBoard _gameBoard;
    private InputHandler _inputHandler;

    [Inject]
    private void Construct(GameplayStateMachine gameplayStateMachine, GameBoard gameBoard, InputHandler inputHandler)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameBoard = gameBoard;
        _inputHandler = inputHandler;
    }
    private void Start()
    {
        _gameBoard.Init();
        _inputHandler.Init();
        _gameplayStateMachine.EnterState<PlayState>();
    }
}
