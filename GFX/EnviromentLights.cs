using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnviromentLights : MonoBehaviour
{
    private Light2D light;
    void Start()
    {
        light = GetComponent<Light2D>();

        if (RaceManager.currentMission == null) return;

        if (RaceManager.currentMission.isNight)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
}
