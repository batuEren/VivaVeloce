using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDestroyer : MonoBehaviour
{
    void Start()
    {
        MusicManager[] obj = FindObjectsOfType<MusicManager>();
        foreach (MusicManager obj2 in obj)
        {
            Destroy(obj2.gameObject);
        }
        
        if (MusicManager.instance != null)
        {
            Destroy(MusicManager.instance.gameObject);
        }
    }
}
