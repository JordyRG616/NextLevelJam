using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Settings/Dropdown Binding")]
public class DropdownSettingBinding : SettingBindingBase
{
    public string key;
    public Signal<int> OnInitialized;
    public Signal<int> OnValueChanged;

    [Header("Options")]
    [SerializeField] private int defaultOptionIndex;
    [field:SerializeField] public List<DropdownOption> options { get; private set; }

    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0, options.Count - 1);
            OnValueChanged.Fire(_value);
            PlayerPrefs.SetInt(key, _value);
        }
    }


    public override void Initialize()
    {
        if (PlayerPrefs.HasKey(key))
        {
            Value = PlayerPrefs.GetInt(key);
        }
        else Value = defaultOptionIndex;
    }

    public void SetOption(List<DropdownOption> newOptions)
    {
        options = newOptions;
    }
}

[System.Serializable]
public struct DropdownOption
{
    public string label;
    public Sprite icon;
}