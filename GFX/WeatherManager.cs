using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public bool deactiveOnRain = false;
    public bool deactivateOnClear = false;
    public bool deactivateOnDay = false;
    public bool deactivateOnNight = false;
    void Start()
    {
        GameObject go = this.gameObject;

        if (RaceManager.currentMission == null) return;

        go.SetActive(true);

        if (RaceManager.currentMission.rainyWeather && deactiveOnRain)
        {
            //Insatntiate Rain
            go.SetActive(false);
        }
        else if(RaceManager.currentMission.isNight && deactivateOnNight)
        {
            //Deinsatntiate Rain
            go.SetActive(false);
        }
        else if (!RaceManager.currentMission.isNight && deactivateOnDay)
        {
            //Deinsatntiate Rain
            go.SetActive(false);
        }
        else if (!RaceManager.currentMission.rainyWeather && deactivateOnClear)
        {
            //Deinsatntiate Rain
            go.SetActive(false);
        }
    }

}
