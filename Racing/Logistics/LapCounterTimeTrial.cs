using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class LapCounterTimeTrial : MonoBehaviour
{
    public TMP_Text lapsText;

    public TimeCheck timeCheck;

    public PlayableDirector endScreen;

    int passedCheckPointNumber = 0;
    float timeAtLastCheckpoint = 0f;

    int numberOfPastCheckPoints = 0;

    int lapsCompleted = 0;

    public int lapsToComplete = 5;

    bool isRaceCompleted = false;

    int carPosition = 0;

    bool isHideRoutineRunning = false;
    float hideUIDelayTime;

    public void Awake()
    {
        lapsText.text = "Laps: " + (lapsCompleted + 1) + "/" + lapsToComplete;
    }

    public void SetCarPosition(int pos)
    {
        carPosition = pos;
    }

    public int GetNumberOfCheckPointsPassed()
    {
        return numberOfPastCheckPoints;
    }

    public float GetTimeAtLastCheckpoint()
    {
        return timeAtLastCheckpoint;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            if (isRaceCompleted)
            {
                return;
            }

            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();

            if (passedCheckPointNumber + 1 == checkpoint.checkPointNumber)
            {
                passedCheckPointNumber = checkpoint.checkPointNumber;

                numberOfPastCheckPoints++;

                Debug.Log(passedCheckPointNumber + " : " + numberOfPastCheckPoints);

                timeAtLastCheckpoint = Time.time;

                if (checkpoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    lapsText.text = "Laps: " + (lapsCompleted + 1) + "/" + lapsToComplete;

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                        lapsText.text = "Race Complete";
                    }
                }

                if (isRaceCompleted)
                {
                    timeCheck.StopTimer();
                    //Start timeline to show time complition.
                    endScreen.Play();
                }
                else
                {
                    
                }
            }
        }
    }
}
