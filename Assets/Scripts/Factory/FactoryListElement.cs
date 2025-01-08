using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public class FactoryListElement<T1, T2>
        where T1 : UnityEngine.Object
        where T2 : Enum
    {
        [field: SerializeField] public T2 Type { get; private set; }
        [field: SerializeField] public T1 Prefab { get; private set; }
    }
}