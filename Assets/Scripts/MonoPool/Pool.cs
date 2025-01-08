using System;
using System.Collections.Generic;
using Factory;
using UnityEngine;

public class MonoPool<T1, T2> 
    where T1 : MonoBehaviour
    where T2 : Enum
{
    private readonly List<IPoolInstance<T1, T2>> _poolInstances = new ();
    private readonly IFactory<T1, T2> _factory;

    public MonoPool(IFactory<T1, T2> factory) => _factory = factory;

    public T1 Get(T2 type)
    {
        foreach (var poolInstance in _poolInstances)
        {
            if (!poolInstance.Free || !poolInstance.Type.Equals(type)) continue;
            poolInstance.ReusePoolInstance();
            return poolInstance.MonoInstance;
        }
        return Create(type);
    }

    public void Clear()
    {
        foreach (var poolInstance in _poolInstances)
            UnityEngine.Object.Destroy(poolInstance.MonoInstance.gameObject);
        _poolInstances.Clear();
    }

    private T1 Create(T2 type)
    {
        var instance = _factory.Create(type);
        var poolInstance = instance as IPoolInstance<T1, T2>;
        poolInstance.Type = type;
        poolInstance.MonoInstance = instance;
        _poolInstances.Add(poolInstance);
        return instance;
    }
}   