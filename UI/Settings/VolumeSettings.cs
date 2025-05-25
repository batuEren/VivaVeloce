using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sfxSlider;
    public Slider musicSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("sfxVol"))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
            SetMusicVolume();
        }
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("sfxVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    private void LoadVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
        musicSlider.value = PlayerPrefs.GetFloat("musicVol");
        SetSFXVolume();
    }
}

