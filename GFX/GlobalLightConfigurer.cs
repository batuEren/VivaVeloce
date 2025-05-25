using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightConfigurer : MonoBehaviour
{
    private Light2D light;
    void Start()
    {
        light = GetComponent<Light2D>();

        if (RaceManager.currentMission == null) return;

        if (RaceManager.currentMission.isNight)
        {
            light.intensity = 0.7f;
        }
        else
        {
            light.intensity = 1.0f;
        }
    }
}
