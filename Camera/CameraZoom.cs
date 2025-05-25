using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Rigidbody2D car;

    public float divider = 15f;

    public float maxZoomOut = 2f;

    private float lensOrtho;
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        lensOrtho = vcam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float extraOrtho = Mathf.Abs(car.velocity.magnitude/divider);
        extraOrtho = Mathf.Clamp(extraOrtho, 0, maxZoomOut);
        
        vcam.m_Lens.OrthographicSize = lensOrtho + extraOrtho;
    }
}
