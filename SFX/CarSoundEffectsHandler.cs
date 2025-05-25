using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundEffectsHandler : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource tiresScreechingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource CarHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTiresScreechingSFX();
    }

    void UpdateEngineSFX()
    {
        //float velocityMagnitude = carController.GetVelocityMagnitude();

        //float desiredEngineVolume = velocityMagnitude * 0.05f;

        float desiredEngineVolume = ((float) carController.engineRPM / (float) carController.maxEngineRPM);

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = ((float)carController.engineRPM / (float)carController.maxEngineRPM) * 2;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tiresScreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 0f, Time.deltaTime * 10);
    }
    void OnCollisionEnter2D(Collision2D collision2d)
    {
        float relativeVelocity = collision2d.relativeVelocity.magnitude;
        float volume = relativeVelocity * 0.1f;

        CarHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
        CarHitAudioSource.volume = volume;

        if(CarHitAudioSource.isPlaying == false)
        {
            CarHitAudioSource.Play();
        }
    }
}
