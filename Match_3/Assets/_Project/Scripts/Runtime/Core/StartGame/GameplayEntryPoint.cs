using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    private GameplayStateMachine _gameplayStateMachine;
    private GameBoard _gameBoard;
    private InputHandler _inputHandler;
    private GameplayUIController _gameplayUIController;
    private TimerController _timerController;
    private ISaveStarategy _saveStarategy = new JsonSaveFromResources();

    [Inject]
    private void Construct(GameplayStateMachine gameplayStateMachine, GameBoard gameBoard, InputHandler inputHandler, GameplayUIController gameplayUIController, TimerController timerController)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameBoard = gameBoard;
        _inputHandler = inputHandler;
        _gameplayUIController = gameplayUIController;
        _timerController = timerController;
    }
    private void Start()
    {
        LevelData levelData = _saveStarategy.Load<LevelData>(SelectedLevel.Level.ToString());

        _gameBoard.Init(levelData);
        _inputHandler.Init();
        _gameplayUIController.Init();
        _timerController.Init(levelData.gameTime);

        _gameplayStateMachine.EnterState<PlayState>();
    }
}
