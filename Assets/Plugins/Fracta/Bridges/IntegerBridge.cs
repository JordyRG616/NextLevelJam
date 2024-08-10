using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Bridge/Integer")]
public class IntegerBridge : ScriptableObject
{
    public Func<int> GetValue;
    public Signal<int> OnValueFetched = new Signal<int>();


    public int Request()
    {
        return GetValue.Invoke();
    }
}
