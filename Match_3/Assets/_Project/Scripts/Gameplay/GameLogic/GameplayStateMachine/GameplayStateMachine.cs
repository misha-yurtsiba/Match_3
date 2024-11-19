using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStateMachine : MonoBehaviour 
{
    private Dictionary<Type, GameState> _gameStates;

    private GameState _curentState;
    public void Init()
    {
        _gameStates = new Dictionary<Type, GameState>();
    }

    public void EnterState<TState>() where TState : GameState
    {
        _curentState?.Exit();
        _curentState = _gameStates[typeof(TState)];
        _curentState.Enter();
    }

    public void AddState<TState>(GameState newState) where TState : GameState
    {
        _gameStates.Add(typeof(TState),newState);
    }
    private void Update()
    {
        _curentState?.Update();
    }
}
