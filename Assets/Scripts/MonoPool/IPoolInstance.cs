using System;
using UnityEngine;

public interface IPoolInstance<T1, T2>
    where T1 : MonoBehaviour
    where T2 : Enum
{
    T2 Type { get; set; }
    T1 MonoInstance { get; set; }
    bool Free { get; set; }

    void ReusePoolInstance();
}