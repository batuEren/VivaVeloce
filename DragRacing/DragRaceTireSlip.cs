using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRaceTireSlip : MonoBehaviour
{
    TrailRenderer trailRenderer;

    DragRaceCarAcc carController;
    DragRaceAI aiController;

    public bool isAi;

    void Start()
    {
        if (!isAi)
        {
            carController = GetComponentInParent<DragRaceCarAcc>();
        }
        else
        {
            aiController = GetComponentInParent<DragRaceAI>();
            Debug.Log("I was here");
        }

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
        
    }

    private void Update()
    {
        if (IsSlipping())
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }

    private bool IsSlipping()
    {
        if (isAi)
        {
            return aiController.isSlipping;
        }
        else
        {
            return carController.isSlipping;
        }
    }
}
