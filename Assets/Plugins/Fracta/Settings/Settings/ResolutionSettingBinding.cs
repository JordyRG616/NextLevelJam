using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Settings/Resolution Binding")]
public class ResolutionSettingBinding : SettingBindingBase
{
    public Signal<ResolutionSettingBinding> OnInitialized;
    public Signal<Resolution> OnValueChanged;

    [Header("Options")]
    private List<ResolutionDropdownOption> options = new List<ResolutionDropdownOption>();
    public string key;

    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0, options.Count - 1);
            OnValueChanged.Fire(options[_value].resolution);

            PlayerPrefs.SetInt(key, _value);
        }
    }

    public override void Initialize()
    {
        options = SettingsManager.GetResolutions();
        var res = Screen.currentResolution;
        var current = options.Find(x => x.resolution.width == res.width && x.resolution.height == res.height);
        if (options.Contains(current))
            Value = options.IndexOf(current);

        if(PlayerPrefs.HasKey(key))
        {
            Value = PlayerPrefs.GetInt(key);
        }
    }

    public List<ResolutionDropdownOption> GetOptions()
    {
        return options;
    }
}

[System.Serializable]
public struct ResolutionDropdownOption
{
    public string label;
    public Resolution resolution;
}