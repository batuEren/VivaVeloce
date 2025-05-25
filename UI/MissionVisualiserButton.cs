using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionVisualiserButton : MonoBehaviour
{
    public MissionAttribute ma;
    public MissionVisualise mv;

    public void EventOnClick()
    {
        mv.VisualiseMission(ma);
        RaceManager.currentMission = ma;
    }
}
