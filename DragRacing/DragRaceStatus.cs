using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DragRaceStatus : MonoBehaviour
{
    private int finalResult = -1; // 0 = loss, 1 = win, 2 = jump, 3 = draw

    public TMP_Text resultText;
    public TMP_Text rewardText;

    public PlayableDirector timeline;

    private bool ended = false;

    void Start()
    {
        
    }

    private void Update()
    {
        DragRaceTimer timer = FindObjectOfType<DragRaceTimer>();
        
        if (timer.playerFinished && timer.opponentFinished)
        {
            BothFinish();
        }
    }

    public void BothFinish() {
        if (DragRaceTimer.playerTime > DragRaceTimer.opponentTime)
        {
            EndRace(0);
        }
        else if (DragRaceTimer.playerTime < DragRaceTimer.opponentTime)
        {
            EndRace(1);
        }
        else if (DragRaceTimer.playerTime == DragRaceTimer.opponentTime)
        {
            EndRace(3);
        }
    }

    public void JumpStart()
    {
        EndRace(2);
    } 

    public void EndRace(int result)
    {
        if (ended) return;

        finalResult = result;

        if (result == -1)
        {
            return;
        }

        if (result == 0)
        {
            resultText.text = "You Lost";
            rewardText.text = "Reward: 0$";
        }
        if (result == 1)
        {
            resultText.text = "You Win";
            rewardText.text = "Reward: 100$";
        }
        if (result == 2)
        {
            resultText.text = "Jump Start";
            rewardText.text = "Reward: 0$";
        }
        if (result == 3)
        {
            resultText.text = "Draw?!?!?";
            rewardText.text = "Reward: 0$";
        }
        timeline.Play();
        ended = true;
    }

    public void CommitResult()
    {
        if(finalResult == 1)
        {
            MoneyManager.money += 100;
        }
    }
}
