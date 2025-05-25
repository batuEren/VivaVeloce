using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMapScroll : MonoBehaviour
{
    public float division;
    private float yAim;
    private int where = 0;


    private void Awake()
    {
        yAim = gameObject.transform.position.y;
    }

    private void Update()
    {
        float y = Mathf.Lerp(gameObject.transform.position.y, yAim, Time.deltaTime*2);
        gameObject.transform.position = new Vector3(transform.position.x,y,transform.position.z);
    }

    public void GoUp()
    {
        if(where <= 0)
        {
            yAim += division;
            where++;
        }
    }

    public void GoDown()
    {
        if (where >= 0)
        {
            yAim -= division;
            where--;
        }
    }
}
