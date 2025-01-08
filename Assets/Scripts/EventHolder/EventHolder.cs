using System;
using UnityEngine;

public class EventHolder 
{
    public event Action Event;

    public void Invoke()
    {
        try
        {
            Event?.Invoke();
        }
        catch(Exception exp)
        {
            Debug.LogError($"Events: {exp.GetType().Name}:{exp.Message}");
        }
    }
}
public class EventHolder <T1>
{
    public event Action<T1> Event;

    public void Invoke(T1 agr1)
    {
        try
        {
            Event?.Invoke(agr1);
        }
        catch (Exception exp)
        {
            Debug.LogError($"Events: {exp.GetType().Name}:{exp.Message}");
        }
    }
}
public class EventHolder <T1, T2>
{
    public event Action<T1, T2> Event;

    public void Invoke(T1 agr1, T2 arg2)
    {
        try
        {
            Event?.Invoke(agr1, arg2);
        }
        catch (Exception exp)
        {
            Debug.LogError($"Events: {exp.GetType().Name}:{exp.Message}");
        }
    }
}
public class EventHolder <T1, T2, T3>
{
    public event Action<T1, T2, T3> Event;

    public void Invoke(T1 agr1, T2 arg2, T3 arg3)
    {
        try
        {
            Event?.Invoke(agr1, arg2, arg3);
        }
        catch (Exception exp)
        {
            Debug.LogError($"Events: {exp.GetType().Name}:{exp.Message}");
        }
    }
}