using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Scriptable Signal")]
public class ScriptableSignal : ScriptableObject
{
    private Signal defaultSignal;
    private Signal<bool> booleanSignal;
    private Signal<float> floatSignal;
    private int customSignalCount;

    private List<CustomSignalHolder> customSignals = new List<CustomSignalHolder>();


    public void ResetToDefault()
    {
        defaultSignal.Clear();
        booleanSignal.Clear();
        floatSignal.Clear();
        customSignals.Clear();
    }

    public CustomSignalHolder<T> CreateCustomSignal<T>()
    {
        var holder = customSignals.Find(x => x.type == typeof(T));

        if(holder != null)
        {
            return holder as CustomSignalHolder<T>;
        }

        var signal = new CustomSignalHolder<T>();
        customSignals.Add(signal);
        Debug.Log(name + " has " + customSignals.Count + " custom signal(s).");

        return signal;
    }

    public void Fire()
    {
        if (defaultSignal == null)
        {
            return;
        }

        defaultSignal.Fire();
    }

    public void Fire(bool value)
    {
        if (booleanSignal == null)
        {
            return;
        }

        booleanSignal.Fire(value);
    }

    public void Fire(float value)
    {
        if (floatSignal == null)
        {
            return;
        }

        floatSignal.Fire(value);
    }

    public void Fire<T>(T value)
    {
        var holder = customSignals.Find(x => x.type == typeof(T));
        var tHolder = holder as CustomSignalHolder<T>;

        if (tHolder == null) return;

        tHolder.signal.Fire(value);
        Debug.Log(name + " was fired");
    }

    public void Register(Action callback)
    {
        if (defaultSignal == null)
        {
            defaultSignal = new Signal();
        }

        defaultSignal += callback;
    }

    public void Delist(Action callback)
    {
        if (defaultSignal == null) return;

        Debug.Log(callback.Method.Name + " was delisted");
        defaultSignal -= callback;
    }

    public void Register(Action<bool> callback)
    {
        if (booleanSignal == null)
        {
            booleanSignal = new Signal<bool>();
        }

        booleanSignal += callback;
    }

    public void Register(Action<float> callback)
    {
        if (floatSignal == null)
        {
            floatSignal = new Signal<float>();
        }

        floatSignal += callback;
    }

    public void Register<T>(Action<T> callback)
    {
        var holder = customSignals.Find(x => x.type == typeof(T));
        var tHolder = holder as CustomSignalHolder<T>;

        Debug.Log(callback.Method.Name + " was registered to " + name);
        if (tHolder == null)
        {
            tHolder = CreateCustomSignal<T>();
        }

        tHolder.signal.Register(callback);
    }

    public void Delist<T>(Action<T> callback)
    {
        var holder = customSignals.Find(x => x.type == typeof(T));
        var tHolder = holder as CustomSignalHolder<T>;

        if (tHolder == null) return;
        
        tHolder.signal -= callback;
    }

    private void OnValidate()
    {
        customSignalCount = customSignals.Count;
    }
}

public abstract class CustomSignalHolder
{
    public Type type;

    public abstract void Fire();
}

[System.Serializable]
public class CustomSignalHolder<T> : CustomSignalHolder
{
    public Signal<T> signal;

    public CustomSignalHolder()
    {
        signal = new Signal<T>();
        type = typeof(T);
    }

    public override void Fire()
    {

    }
}