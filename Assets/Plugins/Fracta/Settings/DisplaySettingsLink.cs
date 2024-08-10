using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySettingsLink : MonoBehaviour
{
    [SerializeField] private ResolutionSettingBinding resolutionBinding;
    [SerializeField] private DropdownSettingBinding displayModeBinding;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown displayDropdown;



    private void Start()
    {
        resolutionBinding.Initialize();

        resolutionDropdown.ClearOptions();
        var list = new List<string>();

        foreach (var resolution in resolutionBinding.GetOptions())
        {
            list.Add(resolution.label);
        }

        resolutionDropdown.AddOptions(list);
        resolutionDropdown.SetValueWithoutNotify(resolutionBinding.Value);

        resolutionBinding.OnValueChanged.Clear();
        resolutionBinding.OnValueChanged += (res) => SettingsManager.SetResolution(res);

        displayModeBinding.OnValueChanged += (value) => SettingsManager.SetDisplayMode(value);
        displayModeBinding.Initialize();
        displayDropdown.SetValueWithoutNotify(displayModeBinding.Value);
    }

}
