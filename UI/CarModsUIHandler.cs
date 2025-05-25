using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarModsUIHandler : MonoBehaviour
{
    private HorizontalLayoutGroup layoutGroup;
    public GameObject modItemPrefab;
    private ModAttributes[] lastMods;

    private void Awake()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public void updateTheMods(ModAttributes[] mods)
    {
        if(mods == null)
        {
            return;
        }
        GameObject g = this.gameObject;
        for (var i = g.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(g.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < mods.Length; i++)
        {
            GameObject infoGameObject = Instantiate(modItemPrefab, layoutGroup.transform);

            ComponentToUI componentToUI = infoGameObject.GetComponent<ComponentToUI>();

            lastMods = mods;

            componentToUI.configure(mods[i]);
        }
    }

    public void refresh()
    {
        updateTheMods(lastMods);
    }
}
