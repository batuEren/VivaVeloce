using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    public static string previousScene = "selectionMap";
    public bool setAsPreviousScene = false;

    void Start()
    {
        if (setAsPreviousScene)
        {
            previousScene = SceneManager.GetActiveScene().name;
        }
    }

    public static void LoadPrevious()
    {
        SceneManager.LoadScene(previousScene);
    }
}
