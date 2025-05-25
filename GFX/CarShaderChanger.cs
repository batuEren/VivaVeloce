using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShaderChanger : MonoBehaviour
{
    private SpriteRenderer carSpriteRenderer;
    
    public Material carDayMaterial;
    public Material carNightMaterial;
    void Awake()
    {
        carSpriteRenderer = GetComponent<SpriteRenderer>();
        
        if (RaceManager.currentMission == null)
        {
            carSpriteRenderer.material = carNightMaterial;
            return;
        }

        if (RaceManager.currentMission.isNight)
        {
            carSpriteRenderer.material = carNightMaterial;
        }
        else
        {
            carSpriteRenderer.material = carDayMaterial;
        }
        
    }

}
