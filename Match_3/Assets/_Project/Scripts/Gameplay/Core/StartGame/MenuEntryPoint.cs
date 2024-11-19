using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuEntryPoint : MonoBehaviour
{
    private MainMenuUIController _mainMenuUIController;

    [Inject]
    private void Construct(MainMenuUIController mainMenuUIController)
    {
        _mainMenuUIController = mainMenuUIController;
    }
    private void Start()
    {
        _mainMenuUIController.ActiveMenuPanel();
    }
}
