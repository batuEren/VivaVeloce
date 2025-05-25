using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitApp()
    {
        Application.Quit();
        Debug.Log("Exitting");
    }
}
