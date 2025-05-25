using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionVisualise : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text rewardText;
    public TMP_Text rankText;

    public Animator animator;

    private bool isDisplayOn = false;

    public void VisualiseMission(MissionAttribute ma)
    {
        nameText.text = ma.name;
        rewardText.text = ma.reward + "$";
        rankText.text = "Class Req: " + ma.requiredRank;

        if (isDisplayOn) return;

        animator.Play("raceDisplayEnter");
        isDisplayOn = true;
    }

    public void ExitScreen()
    {
        if (!isDisplayOn) return;
        
        animator.Play("raceDisplayExit");
        isDisplayOn = false;
    }
}
