using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackToMenu : MonoBehaviour
{
    private ISceneChanger _sceneChanger;
    private SwipeHandler _swipeHandler;

    [SerializeField] private Button _backToMenuButton;

    [Inject]
    private void Construct(ISceneChanger sceneChanger, SwipeHandler swipeHandler)
    {
       _sceneChanger = sceneChanger;
       _swipeHandler = swipeHandler;
    }

    public void Init()
    {
        _backToMenuButton.onClick.AddListener(() =>
        {
            _swipeHandler.Unsubscribe();
            _sceneChanger.ChangeScene(Scenes.Menu);
        });
    }
}
