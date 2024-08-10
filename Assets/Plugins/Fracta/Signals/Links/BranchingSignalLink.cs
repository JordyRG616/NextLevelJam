using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Signal Links/Branching Link")]
public class BranchingSignalLink : MonoBehaviour
{
    public SignalReference<bool> signalReference;
    [SerializeField] private bool invertSignal;
    [Space]
    public UnityEvent<bool> linkedCallback;


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
        if (invertSignal) value = !value;
        linkedCallback?.Invoke(value);
    }
}
