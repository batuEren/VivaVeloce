using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashboardToUI : MonoBehaviour
{
    public TMP_Text speedText;
    public TMP_Text rpmText;
    public RectTransform rod;
    public TMP_Text gearText;
    private CarController carController;
    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = carController.GetSpeedInMPH().ToString("0");
        gearText.text = "G:" + carController.gear;
        float rot = 160f * (float) (carController.engineRPM / carController.maxEngineRPM);

        rot = Mathf.Lerp(rot, 360f - transform.rotation.eulerAngles.z - 130f, Time.deltaTime);

        rpmText.text = carController.engineRPM.ToString("0.0");
        rod.rotation = Quaternion.Euler(0,0,-rot + 130f);
    }
}
