using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    private GameplayStateMachine _gameplayStateMachine;

    [Inject]
    private void Construct(GameplayStateMachine gameplayStateMachine)
    {
        _gameplayStateMachine = gameplayStateMachine;
    }
    private void Start()
    {
        
        _gameplayStateMachine.EnterState<PlayState>();
    }
}
