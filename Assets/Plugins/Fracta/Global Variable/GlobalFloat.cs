using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Fracta/Data/Global Float")]
public class GlobalFloat : ScriptableObject
{
    [SerializeField] private bool clamped;
    [ShowIf("clamped"), SerializeField] private float minValue;
    [ShowIf("clamped"), SerializeField] private float maxValue;
    [Space]
    [SerializeField] private bool observable;
    [ShowIf("observable"), SerializeField] private ScriptableSignal onValueChanged;
    [ShowIf("ShowNormalizedSignal"), SerializeField] private ScriptableSignal onValueChangedNormalized;

    private bool ShowNormalizedSignal => observable && clamped;

    private float _value;
    public float Value
    {
        get => _value;
        set
        {
            if (clamped) value = Mathf.Clamp(value, minValue, maxValue);

            _value = value;

            if (onValueChanged != null) onValueChanged.Fire(_value);
        }
    }

    private void OnValidate()
    {
        if (!observable) onValueChanged = null;
    }

    #region Operators
    public static GlobalFloat operator +(GlobalFloat a, float b)
    {
        a.Value += b;
        return a;
    }

    public static GlobalFloat operator -(GlobalFloat a, float b)
    {
        a.Value -= b;
        return a;
    }

    public static GlobalFloat operator *(GlobalFloat a, float b)
    {
        a.Value *= b;
        return a;
    }

    public static GlobalFloat operator /(GlobalFloat a, float b)
    {
        a.Value /= b;
        return a;
    }

    public static GlobalFloat operator %(GlobalFloat a, float b)
    {
        a.Value %= b;
        return a;
    }
    #endregion

}
