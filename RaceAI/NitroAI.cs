using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class NitroAI : MonoBehaviour
{
    public float nitroAmount = 100f;
    public float maxNitroAmount = 100f;
    public float nitroRecoveryRate = 0.1f;

    private float nitroUseRate = 60f;

    public bool usingNitro = false;

    CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        if (usingNitro && nitroAmount > 0)
        {
            nitroAmount -= nitroUseRate * Time.deltaTime;
            carController.nitroRate = 2f;
        }
        else if (usingNitro)
        {
            carController.nitroRate = 1f;
        }
        else
        {
            carController.nitroRate = 1f;
            if (nitroAmount < maxNitroAmount)
            {
                nitroAmount += nitroRecoveryRate * Time.deltaTime;
            }
        }
    }
}
