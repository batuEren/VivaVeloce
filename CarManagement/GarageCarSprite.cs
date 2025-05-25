using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GarageCarSprite : MonoBehaviour
{
    public Transform[] carPlaces;

    void Awake()
    {
        ArrayList cars = CarManager.cars;
        for(int i = 0; i<carPlaces.Length; i++)
        {
            SpriteRenderer srenderer = carPlaces[i].gameObject.GetComponent<SpriteRenderer>();
            if (i < cars.Count)
            {
                Car car = (Car)cars[i];
                srenderer.sprite = car.carAttributes.image;
            }
            else
            {
                srenderer.sprite = null;
            }
            
            
        }
    }

    public void SelectedCarAnimate()
    {
        Rigidbody2D rb = carPlaces[CarManager.selectedCar].GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.0f;
    }

    public void SelectedCarAnimateDelay()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.2f);
        SelectedCarAnimate();
    }

}
