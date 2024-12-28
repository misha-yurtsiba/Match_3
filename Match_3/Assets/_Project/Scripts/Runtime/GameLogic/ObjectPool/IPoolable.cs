using System;
using UnityEngine;

public interface IPoolable
{
    public GameObject GameObject { get; }

    public event Action<IPoolable> Destroyed;
}
