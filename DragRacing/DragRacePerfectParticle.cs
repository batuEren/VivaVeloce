using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRacePerfectParticle : MonoBehaviour
{
    private DragRaceCarAcc carController;
    public GameObject perfectParticle;

    private bool spawned = false;

    void Start()
    {
        carController = GetComponent<DragRaceCarAcc>();
    }

    
    void Update()
    {
        if(carController.perfect == 2 && !spawned)
        {
            Instantiate(perfectParticle, gameObject.transform);
            spawned = true;
        }
    }
}
