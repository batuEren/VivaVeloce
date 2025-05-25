using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageUpdateSFX : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }
}
