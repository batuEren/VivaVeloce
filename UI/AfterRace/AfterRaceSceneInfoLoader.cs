using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class AfterRaceSceneInfoLoader : MonoBehaviour
{
    public TMP_Text rewardText;
    public TMP_Text netText;
    public TMP_Text buyinText;
    public TMP_Text rankText;
    public TMP_Text posText;
    public TMP_Text timeText;
    public TMP_Text nameOfTheRaceText;


    void Start()
    {
        if (RaceManager.currentMission.isRace)
        {
            timeText.text = "Time: " + RaceResultManager.finishTime.ToString("0.00") + "s";
            rewardText.text = "Reward: " + (RaceManager.currentMission.reward / RaceResultManager.position) + "$";
            rankText.text = "Race Rank: " + RaceManager.currentMission.requiredRank;
            posText.text = "Pos: " + RaceResultManager.position + "/" + RaceResultManager.countOfCars;
            netText.text = "Net: " + (RaceManager.currentMission.reward / RaceResultManager.position) + "$";
            nameOfTheRaceText.text = RaceManager.currentMission.name;
        }
        if (RaceManager.currentMission.isTimeTrial)
        {
            timeText.text = "Time: " + RaceResultManager.finishTime.ToString("0.00") + "s";
            rankText.text = "Race Rank: " + RaceManager.currentMission.requiredRank;
            posText.text = "Target Time: " + RaceManager.currentMission.targetTime.ToString("0.00") + "s";
            int money = 0;
            if(RaceResultManager.finishTime <= RaceManager.currentMission.targetTime)
            {
                money = RaceManager.currentMission.reward;
            }
            rewardText.text = "Reward: " + money + "$";
            netText.text = "Net: " + money + "$";
            nameOfTheRaceText.text = RaceManager.currentMission.name;
        }
    }

    public void RetryRace()
    {
        RaceManager.LoadCurrentRace();
    }

    public void Commit()
    {
        if (RaceManager.currentMission.isRace)
        {
            MoneyManager.money += (RaceManager.currentMission.reward / RaceResultManager.position);
        }

        SceneManager.LoadScene("selectionMap");
    }

}
