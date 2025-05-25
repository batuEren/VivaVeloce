using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLeaderboardItem : MonoBehaviour
{
    public TMP_Text positionText;
    public TMP_Text driverNameText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPositionText(string newPos)
    {
        positionText.text = newPos;
    }

    public void SetDriverName(string driverName)
    {
        driverNameText.text = driverName;
    }
}
