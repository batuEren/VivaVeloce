using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragRaceTimer : MonoBehaviour
{
    TMP_Text timeText;
    private float timeSinceBeginning;

    public bool started = false;
    public bool playerFinished = false;
    public bool opponentFinished = false;

    public static float playerTime;
    public static float opponentTime;

    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        RacingLights racingLights = FindObjectOfType<RacingLights>().GetComponent<RacingLights>();
        racingLights.OnDragRaceLightsOut += DragRaceLightsOut;
    }

    public void DragRaceLightsOut(object obj, EventArgs args)
    {
        started = true; // change for reaction time
    }

    void Update()
    {
        if (started)
        {
            timeSinceBeginning += Time.deltaTime;
        }
        if (!playerFinished)
        {
            timeText.text = "Time: " + timeSinceBeginning.ToString("00.00");
        }
    }

    public void StartTimer()
    {
        started = true;
    }

    public void StopVisibleTimer()
    {
        started = false;
    }

    public void PlayerFinish()
    {
        if (!playerFinished)
        {
            playerTime = timeSinceBeginning;
            playerFinished = true;
        } 
    }

    public void OpponentFinish()
    {
        if (!opponentFinished)
        {
            opponentTime = timeSinceBeginning;
            opponentFinished = true;
        }
    }
}
