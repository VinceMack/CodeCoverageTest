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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //not needed
    // Update is called once per frame
    //void Update()
    //{    
    //}

    
    public void SetVolume(float volume)
    {
        audioSource.volume = volume/100f;
        currentVolume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, 
                resolution.height, Screen.fullScreen);
    }


    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", 
                qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", 
                resolutionDropdown.value);
        PlayerPrefs.SetInt("AntiAliasingPreference", 
                aaDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", 
                Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", 
                currentVolume); 
    }
}
