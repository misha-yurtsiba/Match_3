using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button _stsrtGameButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private SelectLevelView _selectLevelView;

    public void ActiveMenuPanel()
    {
        _selectLevelView.SpawnLevelBattons();
        _stsrtGameButton.onClick.AddListener(() =>
        {
            _selectLevelView.gameObject.SetActive(true);
        });

        _quitGameButton.onClick.AddListener(() => Application.Quit());
    }
}
