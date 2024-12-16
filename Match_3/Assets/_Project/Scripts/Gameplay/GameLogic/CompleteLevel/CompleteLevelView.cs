using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class CompleteLevelView : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton; 
    [SerializeField] private Button _backToMenuButton;

    private ISceneChanger _sceneChanger;

    [Inject]
    private void Construct(ISceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }

    public void Init()
    {
        _nextLevelButton.onClick.AddListener(() =>
        {
            SelectedLevel.Level++;
            _sceneChanger.ChangeScene(Scenes.Gameplay);
        });

        _backToMenuButton.onClick.AddListener(() =>
        {
            _sceneChanger.ChangeScene(Scenes.Menu);
        });
    }

    public void StartActiveAnimation()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(1, 0.75f).SetEase(Ease.OutBack);
    }
}
