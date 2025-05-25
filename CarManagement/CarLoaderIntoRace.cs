using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLoaderIntoRace : MonoBehaviour
{
    public SpriteRenderer carSprite;
    public SpriteRenderer carFaceSprite;

    private CarController carController;

    public float accelerationConstant = 40f;
    public float speedDivisionConstant = 15.5f;

    private Car car;
    
    void Awake()
    {
        car = (Car) CarManager.cars[CarManager.selectedCar];

        carSprite.sprite = car.carAttributes.image;

        if (car.carAttributes.faceImage != null)
        {
            carFaceSprite.sprite = car.carAttributes.faceImage;
            if(car.color != null)
            {
                carFaceSprite.color = car.color;
            }
        }

        carController = GetComponent<CarController>();

        carController.driftFactor = 0.9f + Mathf.Min(3/car.getGrip(), 0.0999f);
        carController.accelerationFactor = (car.getHorsePower() / car.getWeight()) * accelerationConstant;
        Debug.Log("Acc factor for player car: " + ((car.getHorsePower() / car.getWeight()) * accelerationConstant)); // must have diminishing returns
        //carController.turningFactor = car.getGrip()/100;
        carController.maxSpeed = car.getSpeed() / speedDivisionConstant;

    }

}
