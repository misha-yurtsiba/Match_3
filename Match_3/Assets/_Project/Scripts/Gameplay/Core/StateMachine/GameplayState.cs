using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GameplayState : IState
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
}

