using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : ISceneChanger
{
    public static Scenes sceneToChange;

    private readonly StateMachine _stateMachine;

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
