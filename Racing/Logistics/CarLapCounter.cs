using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarLapCounter : MonoBehaviour
{
    public TMP_Text carPosText;
    
    int passedCheckPointNumber = 0;
    float timeAtLastCheckpoint = 0f;

    int numberOfPastCheckPoints = 0;

    int lapsCompleted = 0;

    const int lapsToComplete = 2;

    bool isRaceCompleted = false;

    int carPosition = 0;

    bool isHideRoutineRunning = false;
    float hideUIDelayTime;

    public PositionHandler positionHandler;

    public event Action<CarLapCounter> OnPassCheckpoint;

    public void Awake()
    {
        positionHandler = FindObjectOfType<PositionHandler>().GetComponent<PositionHandler>();
    }

    public void Start()
    {
        if(carPosition != null)
        {
            carPosText.text = "Pos: " + positionHandler.carLapCounters.Count + "/" + positionHandler.carLapCounters.Count;
        }
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

    IEnumerator ShowPositionCO(float delayUntilHidePos)
    {
        hideUIDelayTime += delayUntilHidePos;

        carPosText.text = carPosition.ToString();

        carPosText.gameObject.SetActive(true);

        if (!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;

            yield return new WaitForSeconds(hideUIDelayTime);
            carPosText.gameObject.SetActive(false);

            isHideRoutineRunning = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            if (isRaceCompleted) {
                return;
            }

            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();

            if (passedCheckPointNumber + 1 == checkpoint.checkPointNumber)
            {
                passedCheckPointNumber = checkpoint.checkPointNumber;

                numberOfPastCheckPoints++;

                timeAtLastCheckpoint = Time.time;

                if (checkpoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                    }
                }

                OnPassCheckpoint?.Invoke(this);


                if (carPosText == null)
                {
                    return;
                }

                carPosText.text = "Pos: " + carPosition.ToString() + "/" + positionHandler.carLapCounters.Count;
            }
        }
    }

    public int GetPosition()
    {
        return carPosition;
    }
}
