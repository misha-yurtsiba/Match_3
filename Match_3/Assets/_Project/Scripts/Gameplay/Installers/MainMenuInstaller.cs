using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuUIController _mainMenuUIController;
    public override void InstallBindings()
    {
        BindMainMenuUIController();


    }

    private void BindMainMenuUIController()
    {
        Container
            .BindInterfacesAndSelfTo<MainMenuUIController>()
            .FromInstance(_mainMenuUIController)
            .AsSingle()
            .NonLazy();
    }
}
