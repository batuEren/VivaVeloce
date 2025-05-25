using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DragRaceBar : MonoBehaviour
{
    public Transform barBeginningPlace;
    public Transform barEndingPlace;
    public Transform barTransform;

    public Rigidbody2D carRb;

    [SerializeField] AnimationCurve barPerfectCurve;

    public bool raceStarted = false;

    public DragRaceCarAcc playerCar;

    public TMP_Text gearText;

    public float accelerationConstant = 1f;
    public float gearConstant = 0.2f;

    private float barPlace = 0f;
    public float barSpeed = 5f;

    public int gear = 1;

    private int perfectCounter = 0;

    private float lastPerfect = 0f;

    public float gear1 = 0.05f;
    public float gear2 = 0.07f;
    public float gear3 = 0.1f;
    public float gear4 = 0.13f;
    public float gear5 = 0.16f;
    public float gear6 = 0.19f;

    public Animator perfectAnimator;
    public Animator shifterAnimator;

    private CameraShake cameraS;


    void Start()
    {
        barTransform.position = barBeginningPlace.position;
        cameraS = FindAnyObjectByType<CameraShake>();
    }


    void Update()
    {
        if (!FindObjectOfType<DragRaceCarAcc>().started) // Listen to race start event
        {
            return;
        }
        barTransform.position += new Vector3(GetGearRatio(gear) * Time.deltaTime * 
            Mathf.Abs(barBeginningPlace.position.x -barEndingPlace.position.x) * gearConstant, 0, 0);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShiftGear();
        }

        if(barTransform.position.x >= barEndingPlace.position.x)
        {
            barTransform.position -= new Vector3(GetGearRatio(gear) * Time.deltaTime *
                Mathf.Abs(barBeginningPlace.position.x - barEndingPlace.position.x) * gearConstant, 0, 0);
            if(gear < 6)
            {
                if(playerCar.perfect > 0.1f)
                {
                    lastPerfect = playerCar.perfect;
                }
                playerCar.perfect = 0f;
            }
        }
    }

    public void ShiftGear()
    {
        if (!FindObjectOfType<DragRaceCarAcc>())
        {
            return;
        }

        if(playerCar.perfect <= 0.1f)
        {
            playerCar.perfect = lastPerfect;
        }

        // ADD CAMERA SHAKE

        if (gear >= 6) return;
        gear++;
        gearText.text = gear.ToString();
        playerCar.perfect += barPerfectCurve.Evaluate((barTransform.position.x- barBeginningPlace.position.x)
            / (barEndingPlace.position.x - barBeginningPlace.position.x))/15; // Normalised bar position.
        cameraS.AddShake(barPerfectCurve.Evaluate((barTransform.position.x - barBeginningPlace.position.x)
            / (barEndingPlace.position.x - barBeginningPlace.position.x)));
        if(barPerfectCurve.Evaluate((barTransform.position.x - barBeginningPlace.position.x)
            / (barEndingPlace.position.x - barBeginningPlace.position.x)) > 1)
        {
            perfectAnimator.Play("dragPerfectTextAppear");
            perfectCounter++;
        }
        if(perfectCounter == 5)
        {
            playerCar.perfect = 2;
            playerCar.carMaxSpeedMPH = playerCar.carMaxSpeedMPH * 1.1f;
            perfectCounter++;
        }
        //barTransform.position -= new Vector3((barEndingPlace.position.x-barBeginningPlace.position.x)*4/10,0,0);
        barTransform.position = barBeginningPlace.position;
        PlayGearAnim(gear);
    }

    public float GetRelativeBarPosition()
    {
        return (barTransform.position.x - barBeginningPlace.position.x) / 
            (barEndingPlace.position.x - barBeginningPlace.position.x);
    }

    private float GetGearRatio(int i)
    {
        if(i == 1)
        {
            return gear1;
        }
        else if(i == 2)
        {
            return gear2;
        }
        else if(i == 3)
        {
            return gear3;
        }
        else if(i == 4)
        {
            return gear4;
        }
        else if(i == 5)
        {
            return gear5;
        }
        else if(i == 6)
        {
            return gear6;
        }
        return 0;
    }

    private void PlayGearAnim(int gear)
    {
        if(gear == 2)
        {
            shifterAnimator.Play("shifter1to2");
        }
        else if (gear == 3)
        {
            shifterAnimator.Play("shifter2to3");
        }
        else if (gear == 4)
        {
            shifterAnimator.Play("shifter3to4");
        }
        else if (gear == 5)
        {
            shifterAnimator.Play("shifter4to5");
        }
        else if (gear == 6)
        {
            shifterAnimator.Play("shifter5to6");
        }
    }
}
