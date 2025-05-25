using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDrawerToMap : MonoBehaviour
{
    public GameObject missionPrefab;
    public GameObject endTextPrefab;
    public Sprite[] missionIcons;

    private static bool endOfDemo = false;
    
    void Start()
    {
        DrawMissions();
    }

    void DrawMissions()
    {
        Debug.Log("Active Missions");
        if (MissionManager.activeMissionsList.Count == 0 || endOfDemo)
        {
            GameObject instantiated = (GameObject)GameObject.Instantiate(endTextPrefab);
            instantiated.transform.SetParent(gameObject.transform, true);
            instantiated.transform.localPosition = new Vector3(0, 0, 0);
            FindObjectOfType<MissionManager>().GetComponent<MissionManager>().ResetMission();
            endOfDemo = true;
        }

        foreach (MissionAttribute ma in MissionManager.activeMissionsList)
        {
            GameObject instantiated = (GameObject)GameObject.Instantiate(missionPrefab);
            instantiated.transform.SetParent(gameObject.transform, true);
            instantiated.transform.localPosition = ma.position;
            instantiated.transform.localScale = new Vector3(1, 1, 1);
            MissionVisualiserButton mvb = instantiated.GetComponent<MissionVisualiserButton>();
            mvb.ma = ma;
            mvb.mv = FindObjectOfType<MissionVisualise>().GetComponent<MissionVisualise>();

            Image img = instantiated.GetComponent<Image>();

            img.sprite = missionIcons[ma.image];

            Debug.Log(ma.name);
        }

        

    }
}
