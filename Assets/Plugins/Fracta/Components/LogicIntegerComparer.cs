using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class LogicIntegerComparer : MonoBehaviour
{
    [SerializeField] private int initialValue;

    public UnityEvent OnGreaterThan;
    public UnityEvent OnLesserThan;
    public UnityEvent OnEqualTo;

    private int storedValue;


    private void Start()
    {
        storedValue = initialValue;
    }

    public void Evaluate(int input)
    {
        if (input > storedValue) OnGreaterThan?.Invoke();
        if (input < storedValue) OnLesserThan?.Invoke();
        if (input == storedValue) OnEqualTo?.Invoke();

        storedValue = input;
    }
}
