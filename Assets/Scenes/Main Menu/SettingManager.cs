using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    public static SettingManager settingInstance;

    public AudioSource audioSource;

    public Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    public float volumeValue;

    void Awake()
    {
        if (settingInstance != null && settingInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        settingInstance = this;
        DontDestroyOnLoad(gameObject);

        int currResolutionIdx = 0;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currResolutionIdx = i;
                Debug.Log(i);
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currResolutionIdx;
        resolutionDropdown.RefreshShownValue();
        volumeValue = 0.5f;

    }

    public void setResolution(int resolutionIdx)
    {
        Debug.Log(resolutionIdx);
        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void changeQuality(int qualityIdx)
    {
        QualitySettings.SetQualityLevel(qualityIdx);
    }
    public void setVolume(float volume) 
    {
        volumeValue = volume;
        audioSource.volume = volume;
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
