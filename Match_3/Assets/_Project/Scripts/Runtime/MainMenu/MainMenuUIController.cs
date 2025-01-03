using UnityEngine;
using UnityEngine.UI;


public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button _stsrtGameButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private SelectLevelView _selectLevelView;

    private void Start()
    {
        _selectLevelView.closeButton.onClick.AddListener(CloseLevelPanel);
    }

    public void ActiveMenuPanel()
    {
        _selectLevelView.SpawnLevelBattons();
        _stsrtGameButton.onClick.AddListener(() =>
        {
            _selectLevelView.gameObject.SetActive(true);
        });

        _quitGameButton.onClick.AddListener(() => Application.Quit());
    }

    public void CloseLevelPanel()
    {
        _selectLevelView.gameObject.SetActive(false);
    }
}