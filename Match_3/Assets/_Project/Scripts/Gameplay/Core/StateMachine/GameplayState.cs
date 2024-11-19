using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GameplayState : IState, ITickable
{
    private ISceneChanger _changer;

    [Inject]
    private void Construct(ISceneChanger changer)
    {
        _changer = changer;
    }
    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _changer.ChangeScene(Scenes.Menu);
        }
    }
}
