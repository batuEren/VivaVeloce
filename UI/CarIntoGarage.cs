using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarIntoGarage : MonoBehaviour
{
    public Transform[] carPlaces;

    private ArrayList cars;

    public Transform cameraTransform;

    public TMP_Text carName;
    public TMP_Text carRank;
    public TMP_Text carRankNo;
    public TMP_Text priceText;
    public Image carRankImagePrimary;
    public Image carRankImageSecondary;

    public TMP_Text speedText;
    public Slider speedSlider;

    public TMP_Text powerText;
    public Slider powerSlider;

    public TMP_Text weightText;
    public Slider weightSlider;

    public TMP_Text gripText;
    public Slider gripSlider;

    public TMP_Text requiredRank;
    public Button raceButton;

    private int selectedCar = 0;

    void Start()
    {
        cars = CarManager.cars;
        refresh();
    }

    public void LoadCar(Car car)
    {
        carName.text = car.carAttributes.carName;
        float carRankNumber = car.getRankNo();
        carRankNo.text = carRankNumber.ToString("f0");

        priceText.text = MoneyManager.money.ToString();

        if(carRankNumber >= 500)
        {
            carRank.text = "S";
            carRankImagePrimary.color = Color.magenta; //get the colours spesified
            carRankImageSecondary.color = Color.magenta;
        }
        else if(carRankNumber >= 400)
        {
            carRank.text = "A";
            carRankImagePrimary.color = Color.red;
            carRankImageSecondary.color = Color.red;
        }
        else if (carRankNumber >= 300)
        {
            carRank.text = "B";
            carRankImagePrimary.color = Color.blue;
            carRankImageSecondary.color = Color.blue;
        }
        else if (carRankNumber >= 200)
        {
            carRank.text = "C";
        }
        else if (carRankNumber >= 100)
        {
            carRank.text = "D";
        }
        else
        {
            carRank.text = "E";
        }

        speedText.text = car.getSpeed().ToString("f0") + " mph";
        speedSlider.value = car.getSpeed()/200f;

        powerText.text = car.getHorsePower().ToString("f0") + " HP";
        powerSlider.value = car.getHorsePower()/750;

        weightText.text = car.getWeight().ToString("f0") + " kg";
        weightSlider.value = car.getWeight() / 2500;

        gripText.text = car.getGrip().ToString("f0");
        gripSlider.value = car.getGrip() / 100;

        if (RaceManager.currentMission == null) return;

        if(RaceManager.currentMission.requiredRank != carRank.text)
        {
            raceButton.interactable = false;
            requiredRank.enabled = true;
        }
        else
        {
            raceButton.interactable = true;
            requiredRank.enabled = false;
        }
    }

    public void goRight()
    {
        if(selectedCar >= 2 || selectedCar >= cars.Count - 1)
        {
            return;
        }

        selectedCar++;

        if(selectedCar<cars.Count && selectedCar >= 0) {
            cameraTransform.position = new Vector2(carPlaces[selectedCar].position.x, 0);
            refresh();
        }
    }
    public void refresh()
    {
        if(cars == null || cars.Count == 0)
        {
            cars = CarManager.cars;
        }
        Debug.Log("Selected Car: " + selectedCar);
        Car currentC = (Car)cars[selectedCar];
        CarManager.selectedCar = selectedCar;
        LoadCar(currentC);
        GameObject.FindObjectOfType<CarModsUIHandler>().GetComponent<CarModsUIHandler>().refresh();
    }

    public void goLeft()
    {
        if (selectedCar <= 0)
        {
            return;
        }

        selectedCar--;

        
        if (selectedCar < cars.Count && selectedCar >= 0)
        {
            refresh();
            cameraTransform.position = new Vector2(carPlaces[selectedCar].position.x, 0);
        }
    }
}
