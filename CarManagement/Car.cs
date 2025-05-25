using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public string plateNo;
    public CarAttributes carAttributes;
    public List<ModAttributes> activeMods;
    public List<ModAttributes> passiveMods;

    public Color color;

    public float getHorsePower()
    {
        float hp = carAttributes.horsePower;
        float add = 0f;
        float percent = 1f;
        foreach (ModAttributes m in activeMods)
        {
            add += m.hpIncrease;
            percent += (m.percentageHPincrease / 100f);
        }

        return (hp + add) * percent;
    }

    public float getSpeed()
    {
        float speed = carAttributes.topSpeed;
        float add = 0f;
        float percent = 1f;
        foreach (ModAttributes m in activeMods)
        {
            add += m.speedIncrease;
            percent += (m.percentSpeedIncrease / 100);
        }

        return (speed + add) * percent;
    }

    public float getWeight()
    {
        float weight = carAttributes.weight;
        foreach (ModAttributes m in activeMods)
        {
            weight += m.weightChange;
        }
        return weight;
    }

    public float getGrip()
    {
        float grip = carAttributes.grip;
        foreach (ModAttributes m in activeMods)
        {
            grip += m.gripIncrease;
        }
        return grip;
    }

    public float getRankNo()
    {
        return (getSpeed() + getGrip() * 1.7f) + 250 * getHorsePower() / getWeight();
    }

    public void addMod(){ 

    }
}
