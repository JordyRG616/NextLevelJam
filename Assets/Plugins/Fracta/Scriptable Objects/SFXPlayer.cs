using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(menuName = "Fracta/SFX")]
public class SFXPlayer : ScriptableObject
{
    public EventReference sfx;
    public bool muted;

    public void Play()
    {
        if (muted) return;
        AudioManager.PlayOneshot(sfx);
    }

    public void SetMute(bool mute)
    {
        muted = mute;
    }
}
