using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GarageRaceManager : MonoBehaviour
{
    public TMP_Text requiredRankText;

    public Button startRaceButton;

    void Start()
    {
        if(RaceManager.currentMission != null)
        {
            requiredRankText.text = "Required Rank: " + RaceManager.currentMission.requiredRank;
        }
        else
        {
            requiredRankText.text = "No Race Selected";
            startRaceButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
