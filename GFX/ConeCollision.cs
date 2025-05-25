using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeCollision : MonoBehaviour
{
    private PolygonCollider2D collider;
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("Disable");
    }

    IEnumerator Disable() 
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
    }
}
