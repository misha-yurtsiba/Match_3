using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    public abstract void Enter();
    public abstract void Exit();
    public virtual void Update() { }
}
