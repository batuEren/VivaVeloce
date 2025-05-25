using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public static MissionAttribute currentMission;

    private bool loading = false;

    public static void LoadCurrentRace()
    {
        if(currentMission.sceneName != null)
        {
            SceneManager.LoadScene(currentMission.sceneName);
        }
    }

    public void LoadCurrentRaceWithDelay()
    {
        if (currentMission.sceneName != null)
        {
            StartCoroutine(Loading());
        }
    }

    public static void LoadRace(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private IEnumerator Loading()
    {
        if (loading)
        {
            yield return null;
        }

        loading = true;

        yield return new WaitForSeconds(4f);
        LoadCurrentRace();
    }
}
