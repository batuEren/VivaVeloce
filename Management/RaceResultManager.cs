using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceResultManager : MonoBehaviour
{
    public static int position = 0;
    public static int countOfCars = 0;

    public static float finishTime = 0f;

    public TimeCheck timeCheck;
    public CarLapCounter playerLapCounter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishRace()
    {
        if(playerLapCounter != null)
        {
            position = playerLapCounter.GetPosition();
            countOfCars = playerLapCounter.positionHandler.carLapCounters.Count;
        }

        finishTime = timeCheck.time;
    }
}
