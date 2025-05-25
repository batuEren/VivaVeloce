using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class DragRaceCarAcc : MonoBehaviour
{
    public float carSpeedMPH = 0f;

    public float carMaxSpeedMPH = 200f;
    public float carHP = 1000f;
    public float carWeight = 2000f;
    public float carGrip = 40f;

    [SerializeField] private AnimationCurve accCurve;

    public float accelerationConstant = 1f;

    public float mphToVelocityConstant = 10f;

    private Rigidbody2D carRB;

    public bool started = false;
    private bool ready = false;

    private float startTime;

    public float perfect = 1f;

    public TMP_Text carSpeedText;

    public bool isSlipping = false;


    void Start()
    {
        carRB = GetComponent<Rigidbody2D>();
        RacingLights racingLights = FindObjectOfType<RacingLights>().GetComponent<RacingLights>();
        racingLights.OnReady += DragRaceLightsOut;
    }

    public void StartEngines() // Listen to race start event
    {
        ready = true;
    }

    public void DragRaceLightsOut(object obj, EventArgs args)
    {
        StartEngines();
    }

    void Update()
    {
        if (!ready) return;

        if (Input.GetKeyUp(RacingLights.clutchKey) && !started)
        {
            started = true;
            if(CalculateSlipAmount() != 0)
            {
                StartCoroutine(Slip(CalculateSlipAmount()));
            }
        }
        
        if (!started)
        {
            return;
        }

        if (isSlipping)
        {
            carSpeedMPH += Time.deltaTime * accCurve.Evaluate(carSpeedMPH / carMaxSpeedMPH) *
                (carHP / carWeight) * perfect * accelerationConstant * 0.5f;
        }
        else
        {
            carSpeedMPH += Time.deltaTime * accCurve.Evaluate(carSpeedMPH / carMaxSpeedMPH) *
                (carHP / carWeight) * perfect * accelerationConstant;
        }
        

        carRB.velocity = new Vector2(carSpeedMPH / mphToVelocityConstant, 0);

        if(carSpeedText != null)
        {
            carSpeedText.text = "Speed: " + (int)carSpeedMPH + "mph";
        }
    }

    private float CalculateSlipAmount()
    {
        return Mathf.Max((carHP / carWeight) - (carGrip * 0.01f), 0);
    }

    IEnumerator Slip(float time)
    {
        isSlipping = true;
        yield return new WaitForSeconds(time*3);
        isSlipping = false;
    }
}
