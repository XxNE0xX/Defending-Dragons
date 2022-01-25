using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeAdjustment : MonoBehaviour
{
    private Slider _volumeSlider;
    private float _volume;
    private AudioMixer _audioMixer;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _audioMixer = Resources.Load<AudioMixer>("MainMixer");
        Load();
    }

    private void OnEnable()
    {
        Load();
    }

    public void ChangeVolume(float volume)
    {
        _volume = volume;
        _audioMixer.SetFloat("BGVolume", volume);
        Save();
    }

    private void Load()
    {
        _volume = !PlayerPrefs.HasKey("BGVolume") ? _volumeSlider.maxValue : PlayerPrefs.GetFloat("BGVolume");
        _volumeSlider.value = _volume;
        _audioMixer.SetFloat("BGVolume", _volume);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("BGVolume", _volume);        
    }
}
