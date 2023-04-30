using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Dropdown aaDropdown;
    public Slider volumeSlider;
    float currentVolume;
    Resolution[] resolutions;

    public void SetVolume(float volume)
    {
        audioSource.volume = volume / 100f;
        currentVolume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}