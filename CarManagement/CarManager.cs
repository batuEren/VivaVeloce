using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static ArrayList cars;

    public static int selectedCar = 0;

    public Car[] defaultCars;

    public void Awake() // Later replace with loading cars from config files.
    {
        if (cars == null)
        {
            cars = new ArrayList();
            if(defaultCars != null && defaultCars.Length != 0)
            {
                foreach(Car car in defaultCars)
                {
                    cars.Add(car);
                }
            }
        }
    }
}
