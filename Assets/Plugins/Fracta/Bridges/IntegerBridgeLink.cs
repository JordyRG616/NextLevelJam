using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Bridges/Integer link")]
public class IntegerBridgeLink : MonoBehaviour
{
    [SerializeField] private IntegerBridge bridge;
    [Space]
    public UnityEvent<int> OnValueRequested;


    public void Request()
    {
        var value = bridge.Request();
        Debug.Log(value);
        OnValueRequested?.Invoke(value);
    }
}
