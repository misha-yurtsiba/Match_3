using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : MonoBehaviour, IPoolable
{
    private readonly Queue<IPoolable> _freeObjects;
    private readonly Transform _container;

    private readonly T _objectPrefab;
    
    public ObjectPool(T prefab, int startCapacity)
    {
        _freeObjects = new Queue<IPoolable>(startCapacity);

        _objectPrefab = prefab;

        _container = new GameObject($"{prefab.name}Pool").transform;

        for(int i = 0; i < startCapacity; i++)
            _freeObjects.Enqueue(CreateNewObject());
    }

    public T Get() 
    {
        T poolable = (_freeObjects.Count == 0) ? CreateNewObject() : _freeObjects.Dequeue() as T;

        poolable.Destroyed += Return;
        poolable.GameObject.SetActive(true);

        return poolable;
    } 

    public void Return(IPoolable poolable)
    {
        _freeObjects.Enqueue(poolable);

        poolable.Destroyed -= Return;
        poolable.GameObject.transform.parent = _container;
        poolable.GameObject.SetActive(false);
    }
    private T CreateNewObject() => Object.Instantiate(_objectPrefab, _container);
}