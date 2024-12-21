using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _lockImage;

    public Button Button;

    private LevelButtonController _levelButtonController;
    private TextMeshProUGUI _levelButtonText;
    public void Init(LevelButtonController levelButtonController, bool canActive)
    {
        _levelButtonController = levelButtonController;
        _levelButtonText = GetComponentInChildren<TextMeshProUGUI>();
        _lockImage.gameObject.SetActive(canActive);

        if(!canActive)
            Button.onClick.AddListener(() => _levelButtonController.ChangeLevel());
    }

    public void ShowLevel(int level)
    {
        _levelButtonText.text = $"Level {level}";
    }
}
