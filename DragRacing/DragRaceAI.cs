using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DragRaceAI : MonoBehaviour
{
    public float carSpeedMPH = 0f;

    public float carMaxSpeedMPH = 200f;
    public float carHP = 1000f;
    public float carWeight = 2000f;
    public float carGrip = 40f;

    public float maxReactionTime = 0.3f;
    public float minReactionTime = 0.2f;

    [SerializeField] private AnimationCurve accCurve;

    public float accelerationConstant = 1f;
    
    public float perfect = 1f;

    public float mphToVelocityConstant = 10f;

    private Rigidbody2D carRB;

    private bool started = false;

    public bool isSlipping = false;

    void Start()
    {
        carRB = GetComponent<Rigidbody2D>();
        RacingLights racingLights = FindObjectOfType<RacingLights>().GetComponent<RacingLights>();
        racingLights.OnDragRaceLightsOut += DragRaceLightsOut;
    }

    public void DragRaceLightsOut(object obj, EventArgs args)
    {
        StartCoroutine(StartRace(GenerateReactionTime()));
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;

        //Add reaction time

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
    }

    private float CalculateSlipAmount()
    {
        return Mathf.Max((carHP / carWeight) - (carGrip * 0.01f), 0);
    }

    IEnumerator Slip(float time)
    {
        isSlipping = true;
        yield return new WaitForSeconds(time * 3);
        isSlipping = false;
    }

    IEnumerator StartRace(float time)
    {
        yield return new WaitForSeconds(time);
        started = true;
        if (CalculateSlipAmount() != 0)
        {
            StartCoroutine(Slip(CalculateSlipAmount()));
        }
    }

    private float GenerateReactionTime()
    {
        return UnityEngine.Random.Range(minReactionTime, maxReactionTime);
    }
}
