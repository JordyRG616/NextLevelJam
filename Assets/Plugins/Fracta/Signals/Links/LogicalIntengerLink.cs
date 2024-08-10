using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[AddComponentMenu("Signal Links/Logical Link")]
public class LogicalIntengerLink : MonoBehaviour
{
    private enum Logic { GreaterThan, GreaterOrEqualTo, LesserThan, LesserOrEqualTo, EgualsTo }

    private Func<int, bool> Compare;

    public SignalReference<int> signalReference;
    [SerializeField] private Logic logic;
    [SerializeField] private int compareAgainst;
    [Space]
    public UnityEvent<int> OnTrue;
    public UnityEvent<int> OnFalse;


    private void Awake()
    {
        switch(logic)
        {
            case Logic.GreaterThan:
                Compare = (v) => v > compareAgainst;
                break;
            case Logic.GreaterOrEqualTo:
                Compare = (v) => v >= compareAgainst;
                break;
            case Logic.LesserThan:
                Compare = (v) => v < compareAgainst;
                break;
            case Logic.LesserOrEqualTo:
                Compare = (v) => v <= compareAgainst;
                break;
            case Logic.EgualsTo:
                Compare = (v) => v == compareAgainst;
                break;
        }


        if (signalReference.CreateLink())
        {
            signalReference.Signal += InvokeCallbacks;
        }
        else
        {
        }
    }

    public void InvokeCallbacks(int value)
    {
        if(Compare.Invoke(value))
        {
            OnTrue?.Invoke(value);
        } else
        {
            OnFalse.Invoke(value);
        }
    }
}
