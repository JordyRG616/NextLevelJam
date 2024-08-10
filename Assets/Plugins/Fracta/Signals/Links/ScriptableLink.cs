using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using NaughtyAttributes;

public class ScriptableLink : MonoBehaviour
{
    private enum LinkType { Void, Float, Boolean, InvertedBoolean}

    private bool IsVoid => type == LinkType.Void;
    private bool IsFloat => type == LinkType.Float;
    private bool IsBoolean => type == LinkType.Boolean || type == LinkType.InvertedBoolean;

    [SerializeField] private ScriptableSignal signal;
    [SerializeField] private LinkType type;
    [SerializeField, Label("Signal Callback"), ShowIf("IsVoid")] 
    private UnityEvent OnSignalFired;
    [SerializeField, Label("Signal Callback"), ShowIf("IsFloat")]
    private UnityEvent<float> OnFloatSignalFired;
    [SerializeField, Label("Signal Callback"), ShowIf("IsBoolean")] 
    private UnityEvent<bool> OnBooleanSignalFired;
    public bool initialized;

    private void Awake()
    {
        initialized = true;
        signal.Register((System.Action)Fire);
        signal.Register((System.Action<bool>)Fire);
        signal.Register((System.Action<float>)Fire);
    }

    private void Fire()
    {
        OnSignalFired?.Invoke();
    }

    private void Fire(bool value)
    {
        if (type == LinkType.InvertedBoolean) value = !value;

        OnBooleanSignalFired?.Invoke(value);
    }

    private void Fire(float value)
    {
        OnFloatSignalFired?.Invoke(value);
    }
}