using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : ManagerBehaviour
{
    [SerializeField] private List<AudioChannel> channels;


    private void Start()
    {
        channels.ForEach(x => x.Initialize());
    }

    public static void PlayOneshot(EventReference reference)
    {
        RuntimeManager.PlayOneShot(reference);
    }
}

[System.Serializable]
public class AudioChannel
{
    [SerializeField] private SliderSettingBinding volumeSettingBinding;
    [SerializeField] private ToggleSettingBinding muteSettingBinding;
    [SerializeField] private Slider slider;
    [SerializeField] private string busPath;
    [SerializeField, Range(0, 1)] private float initialVolume;

    private Bus bus;

    private float _volume;
    public float Volume
    {
        get => _volume;
        set
        {
            _volume = Mathf.Clamp01(value);

            bus.setVolume(_volume);
        }
    }


    public void Initialize()
    {
        bus = RuntimeManager.GetBus(busPath);
        Volume = initialVolume;

        volumeSettingBinding.OnValueChanged += ChangeVolume;
        if(muteSettingBinding != null)
            muteSettingBinding.OnValueChanged += (value) => bus.setMute(value);

        volumeSettingBinding.Initialize();
        Volume = volumeSettingBinding.Value;
        slider.SetValueWithoutNotify(Volume);
    }

    private void ChangeVolume(float value)
    {
        Volume = value;
    }
} 