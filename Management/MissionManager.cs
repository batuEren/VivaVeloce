using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public MissionAttribute[] allMissions;

    public MissionAttribute[] activeMissions;

    public static List<MissionAttribute> activeMissionsList;

    void Awake()
    {
        if(activeMissionsList == null)
        {
            activeMissionsList = activeMissions.ToList();
        }
    }

    public void FinishMission(int id)
    {
        Debug.Log("Count of mission: " + activeMissions.Length);

        foreach(MissionAttribute ma in activeMissionsList)
        {
            Debug.Log(id + "checking" + ma.id);
            if (ma.id == id)
            {
                activeMissionsList.Remove(ma);
                Debug.Log("Removed");
                return;
            }
        }
    }

    public void FinishActiveMission()
    {
        FinishMission(RaceManager.currentMission.id);
    }

    public void ResetMission()
    {
        activeMissionsList = activeMissions.ToList();
    }
}


