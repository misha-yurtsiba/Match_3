using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : ISceneChanger
{
    public static Scenes sceneToChange;

    private readonly LoadingCurtain _loadindCurtain;
    private readonly StateMachine _stateMachine;
    private AsyncOperation _loadingAsyncOperation;

    public SceneChanger(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void ChangeScene(Scenes newScene)
    {
        Debug.Log(newScene.ToString());
        sceneToChange = newScene;
        _stateMachine.EnterState<LoadingSceneState>();
    }
}
