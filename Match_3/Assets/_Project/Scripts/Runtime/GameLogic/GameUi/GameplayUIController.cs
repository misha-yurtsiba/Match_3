using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private CompleteLevelView _completeLevelView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private BackToMenu _backToMenu;

    public void Init()
    {
        _gameOverView.Init();
        _completeLevelView.Init();
        _backToMenu.Init();

        _gameOverView.gameObject.SetActive(false);
        _completeLevelView.gameObject.SetActive(false);
    }

    public void ActiveCompleteLevelPanel()
    {
        _completeLevelView.gameObject.SetActive(true);
        _completeLevelView.StartActiveAnimation();
    }

    public void ActiveGameOverPanel()
    {
        _gameOverView.gameObject.SetActive(true);
        _gameOverView.StartActiveAnimation();

    }
}
