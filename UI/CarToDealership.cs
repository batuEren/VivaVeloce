using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CarToDealership : MonoBehaviour
{
    public CarAttributes car;
    private TMP_Text carName;
    public Image carImage;

    void Start()
    {
        carName = GetComponentInChildren<TMP_Text>();

        carName.text = car.carName;
        carImage.sprite = car.image;
    }

}
