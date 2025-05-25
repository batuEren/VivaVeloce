using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRacingCarSFX : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource tiresScreechingAudioSource;
    public AudioSource engineAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    DragRaceCarAcc carController;
    DragRaceBar gearBar;

    void Start()
    {
        carController = GetComponent<DragRaceCarAcc>();
        gearBar = FindObjectOfType<DragRaceBar>().GetComponent<DragRaceBar>();
    }

    void Update()
    {
        UpdateEngineSFX();
        UpdateTireSFX();
    }

    void UpdateEngineSFX()
    {
        float velocityMagnitude = gearBar.GetRelativeBarPosition() * 18f + 3f + gearBar.gear;

        float desiredEngineVolume = velocityMagnitude * 0.05f + gearBar.gear/15;

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 15);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 4.5f);
    }

    void UpdateTireSFX()
    {
        if (carController.isSlipping)
        {
            tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
            tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
        }
        else { 
            tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 0f, Time.deltaTime * 10); 
        }
    }
}
