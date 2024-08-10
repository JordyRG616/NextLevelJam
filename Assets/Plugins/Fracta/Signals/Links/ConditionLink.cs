using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ConditionLink : MonoBehaviour
{
    public SignalReference<bool> signalReference;
    [SerializeField] private bool invertSignal;
    [Space]
    public UnityEvent OnTrueCallback;
    public UnityEvent OnFalseCallback;


    private void Awake()
    {
        if (signalReference.CreateLink())
        {
            signalReference.Signal += InvokeCallbacks;
        }
        else
        {
        }
    }

    private void InvokeCallbacks(bool value)
    {
        if (value) OnTrueCallback?.Invoke();
        else OnFalseCallback?.Invoke();
    }
}
