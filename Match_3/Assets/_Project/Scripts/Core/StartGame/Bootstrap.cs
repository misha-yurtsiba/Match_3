using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Bootstrap : MonoBehaviour
{
    private ISceneChanger _sceneChanger;

    [Inject]
    private void Construct(ISceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        _sceneChanger.ChangeScene(Scenes.Menu);
    }
}
