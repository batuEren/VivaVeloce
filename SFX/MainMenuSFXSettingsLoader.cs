using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class MainMenuSFXSettingsLoader : MonoBehaviour
{
    public AudioMixer mixer;
    void Start()
    {
        if (PlayerPrefs.HasKey("sfxVol"))
        {
            float volume = PlayerPrefs.GetFloat("sfxVol");
            mixer.SetFloat("sfxVol", Mathf.Log10(volume) * 20);

            volume = PlayerPrefs.GetFloat("musicVol");
            mixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
        }
    }

}
