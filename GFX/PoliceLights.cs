using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PoliceLights : MonoBehaviour
{
    private Light2D light;
    public float alternateSpeed = 1f;
    void Awake()
    {
        light = GetComponent<Light2D>();
        StartCoroutine(openLight());
    }


    IEnumerator openLight()
    {
        yield return new WaitForSeconds(alternateSpeed);
        light.enabled = true;
        StartCoroutine(closeLight());
    }

    IEnumerator closeLight()
    {
        yield return new WaitForSeconds(alternateSpeed);
        light.enabled = false;
        StartCoroutine(openLight());
    }
}
