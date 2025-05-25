using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragRaceFinalTimeToText : MonoBehaviour
{
    public bool playerTime = true;

    TMP_Text timeText;

    void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (playerTime)
        {
            timeText.text = "Your Time:\n" + DragRaceTimer.playerTime.ToString("0.00");
        }
        else
        {
            timeText.text = "Opp. Time:\n" + DragRaceTimer.opponentTime.ToString("0.00");
        }
    }
}
