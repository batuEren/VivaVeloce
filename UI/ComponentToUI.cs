using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class ComponentToUI : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text description;
    public TMP_Text priceText;
    public TMP_Text buttonText;

    private ModAttributes attachedMod;
    private int buttonMode = -1;
    private int price;
    private Car currentCar;

    public void configure(ModAttributes mod)
    {
        name.text = mod.name;
        description.text = mod.description;
        priceText.text = "$" + mod.price;
        this.price = mod.price;
        attachedMod = mod;


        currentCar = (Car)CarManager.cars[CarManager.selectedCar];

        if (currentCar.activeMods.Contains(attachedMod))
        {
            buttonText.text = "Unequip";
            buttonMode = 0;
        }
        else if (currentCar.passiveMods.Contains(attachedMod))
        {
            buttonText.text = "Equip";
            buttonMode = 1;
        }
        else
        {
            buttonText.text = "Buy";
            buttonMode = 2;
        }
    }

    public void ButtonAction()
    {
        currentCar = (Car)CarManager.cars[CarManager.selectedCar];
        if (buttonMode == 0)
        {
            for(int i = 0; i<currentCar.activeMods.Count; i++)
            {
                if (currentCar.activeMods[i] == attachedMod) // Might wanna change from list to hashset in future
                {
                    currentCar.activeMods.RemoveAt(i);
                }
            }
            currentCar.passiveMods.Add(attachedMod);
        }

        if (buttonMode == 1)
        {
            for (int i = 0; i < currentCar.passiveMods.Count; i++)
            {
                if (currentCar.passiveMods[i] == attachedMod) // Might wanna change from list to hashset in future
                {
                    currentCar.passiveMods.RemoveAt(i);
                }
            }
            for (int i = 0; i < currentCar.activeMods.Count; i++)
            {
                if (currentCar.activeMods[i].modType == attachedMod.modType) // Might wanna change from list to hashset in future
                {
                    currentCar.passiveMods.Add(currentCar.activeMods[i]);
                    currentCar.activeMods.RemoveAt(i);
                }
            }
            currentCar.activeMods.Add(attachedMod);
        }
        if(buttonMode == 2)
        {
            if(MoneyManager.money < price)
            {
                //Play not enough money sound
                return;
            }
            for (int i = 0; i < currentCar.activeMods.Count; i++)
            {
                if (currentCar.activeMods[i].modType == attachedMod.modType) // Might wanna change from list to hashset in future
                {
                    currentCar.passiveMods.Add(currentCar.activeMods[i]);
                    currentCar.activeMods.RemoveAt(i);
                }
            }
            MoneyManager.money -= price;
            currentCar.activeMods.Add(attachedMod);
        }
        GameObject.FindObjectOfType<CarIntoGarage>().GetComponent<CarIntoGarage>().refresh();
    }
}
