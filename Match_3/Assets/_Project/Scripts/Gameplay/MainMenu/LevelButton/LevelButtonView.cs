using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonView : MonoBehaviour
{
    public Button Button;

    private LevelButtonPresenter _presenter;
    private TextMeshProUGUI _levelButtonText;

    public void Init(LevelButtonPresenter presenter)
    {
        _presenter = presenter;
        _levelButtonText = GetComponentInChildren<TextMeshProUGUI>();

        Button.onClick.AddListener(() => _presenter.ChangeLevel());
    }

    public void ShowLevel(int level)
    {
        _levelButtonText.text = $"Level {level}";
    }
}
