using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private string sceneToLoad;
    
    public void ReplayCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   /*public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadScene()
    {
        if(sceneToLoad != "" && sceneToLoad != null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    public void SetLoadScene(string scene)
    {
        sceneToLoad = scene;
    }
}
