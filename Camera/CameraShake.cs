using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private float frequency = 0f;
    public float freqDropOff;
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;
   
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        m_MultiChannelPerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        frequency = frequency - freqDropOff*Time.deltaTime;
        frequency = Mathf.Clamp(frequency, 0f, 2f);
        m_MultiChannelPerlin.m_FrequencyGain = frequency;
    }

    public void AddShake(float shake)
    {
        frequency += shake;
    }
}
