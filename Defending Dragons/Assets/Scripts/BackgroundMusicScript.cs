using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicScript : MonoBehaviour
{
    public static BackgroundMusicScript BGMusicInstance;

    private void Awake()
    {
        if (BGMusicInstance != null && BGMusicInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        BGMusicInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // For some unknown reason, for this method to work, it had to be in Start and not in Awake! :/
        InitialVolumeSet();
    }

    /// <summary>
    /// The starting volume needs to be set here, since the volumeAdjustment script is on
    /// volumeSlider UI, and won't be loaded in the beginning
    /// </summary>
    private void InitialVolumeSet()
    {
        AudioMixer audioMixer = Resources.Load<AudioMixer>("MainMixer");
        float volume = !PlayerPrefs.HasKey("BGVolume") ? 20 : PlayerPrefs.GetFloat("BGVolume");
        audioMixer.SetFloat("BGVolume", volume);
    }
}
