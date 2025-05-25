using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    private SpriteRenderer thisSprite;
    private SpriteRenderer parentSprite;

    public Vector3 offset = new Vector3(-0.05f, -0.05f);


    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        parentSprite = transform.parent.GetComponent<SpriteRenderer>();

        thisSprite.sprite = parentSprite.sprite;

        this.transform.localPosition = offset;

        thisSprite.sortingLayerName = parentSprite.sortingLayerName;
        thisSprite.sortingOrder = parentSprite.sortingOrder -1;
    }

    void LateUpdate()
    {
        this.transform.position = parentSprite.transform.position + offset;
    }
}
