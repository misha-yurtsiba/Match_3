using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button _stsrtGameButton;
    [SerializeField] private Button _quitGameButton;

    private ISceneChanger _sceneChanger; 

    [Inject]
    private void Construct(ISceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }
    public void ActiveMenuPanel()
    {
        _stsrtGameButton.onClick.AddListener(() =>
        {
            _sceneChanger.ChangeScene(Scenes.Gameplay);
        });

        _quitGameButton.onClick.AddListener(() => Application.Quit());
    }
}
