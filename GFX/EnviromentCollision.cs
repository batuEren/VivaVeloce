using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class EnviromentCollision : MonoBehaviour
{

    public float slowdownFactor = 0.7f;

    public float collisionTriggerSpeed = 3f;

    private SpriteRenderer renderer;
    private PolygonCollider2D collider;
    private Animator animator;
    public bool hasAnimator;
    private ShadowCaster2D shadowCaster;
    public bool hasShadowCaster;
    private Light2D light;
    public bool hasLight;

    public Transform particleLocation;

    public GameObject particleEffect;

    private CameraShake vcam;

    private ControllerRumble rumbler;

    private bool collided;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();
        if (hasAnimator) animator = GetComponent<Animator>();
        if (hasShadowCaster) shadowCaster = GetComponent<ShadowCaster2D>();
        if (hasLight) light = GetComponentInChildren<Light2D>();
        vcam = GameObject.FindGameObjectWithTag("VCam").GetComponent<CameraShake>();
        rumbler = FindObjectOfType<ControllerRumble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        try
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if(Mathf.Abs(rb.velocity.magnitude) < collisionTriggerSpeed)
            {
                return;
            }
            rb.velocity = rb.velocity * new Vector2(slowdownFactor, slowdownFactor);
        }
        catch
        {
            Debug.Log("No Rigidbody found on collision");
            return;
        }

        collider.enabled = false;
        collided = true;
        if (hasAnimator) animator.SetBool("collided", true);
        if (hasShadowCaster) shadowCaster.enabled = false;
        if (hasLight) light.enabled = false;

        if (particleEffect != null)
        {
            Instantiate(particleEffect, gameObject.transform.position, Quaternion.identity);
        }

        renderer.renderingLayerMask = 1;
        if(collision.tag == "Player")
        {
            vcam.AddShake(0.7f);
            rumbler.Rumble(0.2f);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude < collisionTriggerSpeed)
        {
            return;
        }
        try
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();   
            rb.velocity = rb.velocity * new Vector2(slowdownFactor, slowdownFactor);
        }
        catch
        {
            Debug.Log("No Rigidbody found on collision");
            return;
        }

        collider.enabled = false;
        collided = true;
        if (hasAnimator) animator.SetBool("collided", true);
        if (hasShadowCaster) shadowCaster.enabled = false;
        if (hasLight) light.enabled = false;

        if (particleEffect != null)
        {
            Instantiate(particleEffect, particleLocation.position, Quaternion.identity);
        }

        renderer.sortingLayerID = SortingLayer.NameToID("Default");
        if (collision.transform.tag == "Player")
        {
            vcam.AddShake(0.7f);
            rumbler.Rumble(0.2f);
        }
    } 

}
