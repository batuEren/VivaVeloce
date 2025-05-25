using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRaceSmokeParticle : MonoBehaviour
{
    float particleEmissionRate = 0f;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSysytemEmissionModule;

    public bool isAi = false;

    private DragRaceCarAcc carController;
    private DragRaceAI aiController;

    void Awake()
    {
        particleSystemSmoke = GetComponent<ParticleSystem>();

        particleSysytemEmissionModule = particleSystemSmoke.emission;

        particleSysytemEmissionModule.rateOverTime = 0f;
        if (!isAi)
        {
            carController = GetComponentInParent<DragRaceCarAcc>();
        }
        else
        {
            aiController = GetComponentInParent<DragRaceAI>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSysytemEmissionModule.rateOverTime = particleEmissionRate;

        if (IsSlipping())
        {
            particleEmissionRate = 50;
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
