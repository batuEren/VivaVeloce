using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MissionInfo", menuName = "UI/MissionAttribute")]
public class MissionAttribute : ScriptableObject
{
    public string name;
    public int id;
    public int image;
    public int reward;
    public string requiredRank;
    public bool isNight = false;
    public bool rainyWeather = false;
    public string sceneName;
    public bool isTimeTrial;
    public bool isRace;
    public bool isDragRace;
    public float targetTime;
    public Vector2 position;
}