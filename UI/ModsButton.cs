using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModsButton : MonoBehaviour
{
    public ModAttributes[] mods;
    public CarModsUIHandler uIHandler;

    public void ClickAction()
    {
        uIHandler.updateTheMods(mods);
    }
}
