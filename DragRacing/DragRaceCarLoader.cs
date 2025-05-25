using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRaceCarLoader : MonoBehaviour
{
    private SpriteRenderer carSprite;

    private DragRaceCarAcc carController;

    private Car car;

    void Awake()
    {
        car = (Car)CarManager.cars[CarManager.selectedCar];
        carSprite = GetComponent<SpriteRenderer>();

        carSprite.sprite = car.carAttributes.image;

        carController = GetComponent<DragRaceCarAcc>();

        carController.carMaxSpeedMPH = car.getSpeed();
        carController.carHP = car.getHorsePower();
        carController.carWeight = car.getWeight();
        carController.carGrip = car.getGrip();

    }
}
