using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class RacingLights : MonoBehaviour
{
    
    public event EventHandler OnDragRaceLightsOut;
    public event EventHandler OnReady;

    public static KeyCode clutchKey = KeyCode.Space;

    private PlayableDirector mDirector;

    private DragRaceStatus mDragRaceStatus;

    private float timer = 0f;

    public bool ready = false;
    public bool started = false;

    private bool played = false;

    public TMP_Text readyText; 

    void Awake()
    {
        mDirector = GetComponent<PlayableDirector>();
        mDragRaceStatus = FindObjectOfType<DragRaceStatus>().GetComponent<DragRaceStatus>();
    }

  
    void Update()
    {
        if (Input.GetKey(clutchKey))
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if(timer >= 1 && !ready)
        {
            mDirector.Play();
            readyText.text = "Ready!\nRelease to go";
            Ready();
            ready = true;
        }
        if(FindObjectOfType<DragRaceCarAcc>().started == true && !started)
        {
            mDragRaceStatus.JumpStart();
        }
    }

    public void Ready()
    {
        if (OnDragRaceLightsOut != null) OnReady(this, EventArgs.Empty);
    }

    public void StartRace()
    {
        if(OnDragRaceLightsOut != null) OnDragRaceLightsOut(this, EventArgs.Empty);
        readyText.text = "";
        started = true;
    }

}
