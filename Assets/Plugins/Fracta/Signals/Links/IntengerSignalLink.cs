using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Signal Links/Intenger Bridge Link")]
public class IntengerSignalLink : MonoBehaviour
{
    public SignalReference<int> signalReference;
    [Space]
    public UnityEvent<int> linkedCallback;


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

    private void InvokeCallbacks(int value)
    {
        linkedCallback?.Invoke(value);
    }
}
