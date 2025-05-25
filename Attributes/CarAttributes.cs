using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="CarModel", menuName = "CarInfo/CarModel")]
public class CarAttributes : ScriptableObject
{
    public string carName;
    public float topSpeed;
    public float horsePower;
    public float weight;
    public float grip;
    public int carRank;
    public Sprite image;
    public Sprite faceImage;
}