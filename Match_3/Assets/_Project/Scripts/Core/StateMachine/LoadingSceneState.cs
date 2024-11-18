using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadingSceneState : IState
{
    private readonly LoadingCurtain _loadindCurtain;
    private readonly StateMachine _stateMachine;

    private AsyncOperation _loadingAsyncOperation;

    public LoadingSceneState(LoadingCurtain loadindCurtain, StateMachine stateMachine)
    {
        _loadindCurtain = loadindCurtain;
        _stateMachine = stateMachine;
    }
    public void Enter()
    {
        _loadindCurtain.ActiveLoadingCurtain(ChangeScene);
        Debug.Log("Enter");
    }

    public void Exit()
    {

    }

    private async void ChangeScene()
    {
        _loadingAsyncOperation = SceneManager.LoadSceneAsync(SceneChanger.sceneToChange.ToString());

        do
        {
            await Task.Delay(25);
        }
        while (!_loadingAsyncOperation.isDone);

        _loadindCurtain.DisactiveLoadingCurtain(() =>
        {
            switch (SceneChanger.sceneToChange)
            {
                case Scenes.Menu :
                    _stateMachine.EnterState<MenuState>();
                    break;
                case Scenes.Gameplay :
                    _stateMachine.EnterState<GameplayState>();
                    break;
            }
        });
    }
}
