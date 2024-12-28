using System;
using UnityEngine;

public abstract class ExplosionParlicle : MonoBehaviour, IPoolable
{
    protected ParticleSystem _explosion; 
    
    public event Action<IPoolable> Destroyed;
    public GameObject GameObject => gameObject;

    public virtual void Play()
    {
        _explosion ??= GetComponent<ParticleSystem>();
        _explosion.Play();
    }

    private void OnDisable() => Destroyed?.Invoke(this);

}
