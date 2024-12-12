public class LevelButtonPresenter
{
    private LevelButtonModel _levelButtonModel;
    private LevelButtonView _levelButtonView;
    private ISceneChanger _sceneChanger;

    public LevelButtonPresenter(LevelButtonModel levelButtonModel, LevelButtonView levelButtonView, ISceneChanger sceneChanger)
    {
        _levelButtonModel = levelButtonModel;
        _levelButtonView = levelButtonView;
        _sceneChanger = sceneChanger;
    }

    public void ChangeLevel()
    {
        SelectedLevel.Level = _levelButtonModel.Level;
        _sceneChanger.ChangeScene(Scenes.Gameplay);
    }

    public void ShowLevel()
    {
        _levelButtonView.ShowLevel(_levelButtonModel.Level);
    }
}
