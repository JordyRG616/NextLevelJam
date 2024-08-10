using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager 
{
    public static List<ResolutionDropdownOption> GetResolutions()
    {
        var resolutions = Screen.resolutions;
        var list = new List<ResolutionDropdownOption>();

        foreach (var resolution in resolutions)
        {
            if (CheckAspectRatio(resolution.width, resolution.height))
            {
                var option = new ResolutionDropdownOption();
                option.label = resolution.width + "x" + resolution.height;
                option.resolution = resolution;
                list.Add(option);
            }
        }

        Debug.Log(list.Count);
        return list;
    }

    private static bool CheckAspectRatio(float width, float height)
    {
        var ratio = width / height;

        if (ratio > 1.8) return false;
        if (ratio < 1.7) return false;

        return true;
    }

    public static void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public static void SetDisplayMode(int modeIndex)
    {
        if (modeIndex == 2) modeIndex++;
        var mode = (FullScreenMode)modeIndex;

        var res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, mode);
    }
}
