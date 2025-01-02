using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    private GameplayStateMachine _gameplayStateMachine;
    private GameBoard _gameBoard;
    private InputHandler _inputHandler;
    private GameplayUIController _gameplayUIController;
    private TimerController _timerController;
    private CameraPositioner _cameraPositioner;
    private ISaveStarategy _saveStarategy;

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
        _saveStarategy = new JsonSaveFromResources();
        _cameraPositioner = new CameraPositioner();

        LevelData levelData = _saveStarategy.Load<LevelData>(SelectedLevel.Level.ToString());
        levelData ??= _saveStarategy.Load<LevelData>((SelectedLevel.Level -= 1).ToString());

        _gameBoard.Init(levelData);
        _inputHandler.Init();
        _gameplayUIController.Init();
        _timerController.Init(levelData.gameTime);
        _cameraPositioner.SetPosition(_gameBoard.x, _gameBoard.y, -17f);
        _gameplayStateMachine.EnterState<PlayState>();
    }
}
