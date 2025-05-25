using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarCanMove : MonoBehaviour
{
    public CarAIHandler[] aiCars;
    
    public void MakeCanMove()
    {
        foreach(CarAIHandler ai in aiCars)
        {
            ai.MakeCanMove();
        }
    }

    public void MakeCanNotMove()
    {
        foreach (CarAIHandler ai in aiCars)
        {
            ai.MakeCanNotMove();
        }
    }
}
