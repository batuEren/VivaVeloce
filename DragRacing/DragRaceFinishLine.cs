using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRaceFinishLine : MonoBehaviour
{
    public DragRaceTimer timer;
    private BoxCollider2D collider;

    public void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            timer.PlayerFinish();
        }
        if (collision.tag == "Opponent")
        {
            timer.OpponentFinish();
        }
    }
}
