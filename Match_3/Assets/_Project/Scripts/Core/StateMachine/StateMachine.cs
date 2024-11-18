using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private readonly Dictionary<Type, IState> _states;

    private IState _curentState;
    public StateMachine()
    {
        _states = new Dictionary<Type, IState>();
    }
    public void EnterState<TState>() where TState : IState
    {
        _curentState?.Exit();
        _curentState = _states[typeof(TState)];
        _curentState.Enter();
    }

    public void AddState<TState>(IState newState) where TState : IState
    {
        _states.Add(typeof(TState), newState);
    }

}
