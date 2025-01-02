public class CompleteLevelState : GameState
{
    private readonly GameplayUIController _gameplayUIController;
    private readonly TimerController _timerController;
    public CompleteLevelState(GameplayUIController gameplayUIController, TimerController timerController)
    {
        _gameplayUIController = gameplayUIController;
        _timerController = timerController;
    }

    public override void Enter()
    {
        _gameplayUIController.ActiveCompleteLevelPanel();
        _timerController.canUpdateTimer = false;
    }

    public override void Exit()
    {
    }
}
