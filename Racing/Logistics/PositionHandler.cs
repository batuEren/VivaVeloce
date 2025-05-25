using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    //LeaderboardUIHandler leaderboardUIHandler;
    void Awake()
    {
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        foreach (CarLapCounter lapCounters in carLapCounters)
        {
            lapCounters.OnPassCheckpoint += OnPassCheckpoint;
        }

        //leaderboardUIHandler = FindObjectOfType<LeaderboardUIHandler>();
    }

    private void Start()
    {
        //leaderboardUIHandler.UpdateList(carLapCounters);
    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        //Debug.Log($"Event: Car {carLapCounter.gameObject.name} passed a checkpoint.");

        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointsPassed()).
            ThenBy(s => s.GetTimeAtLastCheckpoint()).ToList();

        int carPos = carLapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPosition(carPos);

        //leaderboardUIHandler.UpdateList(carLapCounters);
    }
}
