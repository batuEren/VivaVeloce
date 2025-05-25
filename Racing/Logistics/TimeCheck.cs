using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCheck : MonoBehaviour
{
    private TMP_Text timeText;

    public float time = 0f;

    public bool raceStarted = false;

    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        //StartRace();
    }

    // Update is called once per frame
    void Update()
    {
        if (raceStarted)
        {
            time += Time.deltaTime;
        }
        
        timeText.text = "Time: " + time.ToString("0.00");
    }

    public void StartRace()
    {
        raceStarted = true;
    }

    public void StopTimer()
    {
        raceStarted = false;
    }
}
