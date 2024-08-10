using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Settings/Toggle Binding")]
public class ToggleSettingBinding : SettingBindingBase
{
    public Signal<bool> OnInitialized;
    public Signal<bool> OnValueChanged;

    [Header("Initial Value")]
    [SerializeField] private bool defaultValue;

    private bool _value;
    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged.Fire(_value);
        }
    }

    public override void Initialize()
    {
    }
}
