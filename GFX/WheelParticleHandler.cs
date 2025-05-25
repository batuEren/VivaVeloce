using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0f;

    CarController carController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSysytemEmissionModule;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();

        particleSystemSmoke = GetComponent<ParticleSystem>();

        particleSysytemEmissionModule = particleSystemSmoke.emission;

        particleSysytemEmissionModule.rateOverTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSysytemEmissionModule.rateOverTime = particleEmissionRate;

        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                particleEmissionRate = 50;
            }
            else
            {
                particleEmissionRate = Mathf.Abs(lateralVelocity) * 7;
            }
        }
    }
}
