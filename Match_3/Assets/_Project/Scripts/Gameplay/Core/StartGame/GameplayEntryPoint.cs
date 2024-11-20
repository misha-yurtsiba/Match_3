using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    private GameplayStateMachine _gameplayStateMachine;
    private GameBoard _gameBoard;


    [Inject]
    private void Construct(GameplayStateMachine gameplayStateMachine, GameBoard gameBoard)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameBoard = gameBoard;
    }
    private void Start()
    {
        _gameBoard.Init();
        _gameplayStateMachine.EnterState<PlayState>();
    }
}
