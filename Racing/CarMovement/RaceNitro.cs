using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RaceNitro : MonoBehaviour
{
    CarController carController;

    public Slider nitroSlider;

    public float nitroAmount = 100f;
    public float maxNitroAmount = 100f;
    public float nitroRecoveryRate = 0.1f;

    private float nitroUseRate = 60f;

    public InputActionReference nitroInput;

    void Start()
    {
        carController = GetComponent<CarController>();
        nitroSlider = FindObjectOfType<Slider>().GetComponent<Slider>();
    }

    void Update()
    {
        if (nitroInput.action.IsPressed() && nitroAmount > 0)
        {
            nitroAmount -= nitroUseRate * Time.deltaTime;
            carController.nitroRate = 2f;
        }
        else if (nitroInput.action.IsPressed())
        {
            carController.nitroRate = 1f;
        }
        else
        {
            carController.nitroRate = 1f;
            if(nitroAmount < maxNitroAmount)
            {
                nitroAmount += nitroRecoveryRate * Time.deltaTime;
            }   
        }
        nitroSlider.value = nitroAmount;
    }
}
