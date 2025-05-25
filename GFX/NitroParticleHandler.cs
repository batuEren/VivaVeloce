using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0f;

    CarController carController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSysytemEmissionModule;

    void Start()
    {
        carController = GetComponentInParent<CarController>();

        particleSystemSmoke = GetComponent<ParticleSystem>();

        particleSysytemEmissionModule = particleSystemSmoke.emission;

        particleSysytemEmissionModule.rateOverTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSysytemEmissionModule.rateOverTime = particleEmissionRate;

        if(carController.nitroRate > 1.01f)
        {
            particleEmissionRate = 300;
        }
        else
        {
            //particleEmissionRate = 0;
        }
    }
}
