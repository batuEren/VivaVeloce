using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Modification", menuName = "CarInfo/Modification")]
public class ModAttributes : ScriptableObject
{
    public enum ModType
    {
        Turbo,
        Intake,
        Intercooler,
        Exhaust,
        ECU,
        Transmission,
        Tires
    }

    public string name;
    public string description;
    public float percentageHPincrease;
    public float hpIncrease;
    public float percentSpeedIncrease;
    public float speedIncrease;
    public float weightChange;
    public float gripIncrease;

    [SerializeField]public ModType modType;

    public int price;
}
