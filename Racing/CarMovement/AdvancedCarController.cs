using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30f;
    public float turningFactor = 3.5f;
    public float maxSpeed = 10f;

    private float accelerationInput = 0;
    private float turningInput = 0;

    private float rotationAngle = 0;

    public float nitroRate = 1f;

    private float velocityVsUp;

    private int gear = 1;
    private int maxGear = 6;
    private bool isShifting = false;
    public float shiftingDelay = 0.2f;

    private double engineRPM = 1000.0;
    private double maxEngineRPM = 7000.0;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void CalculateGearAndRMP()
    {
        gear = Mathf.Max((int) Mathf.Ceil(GetVelocityMagnitude() * 6 / maxSpeed), 1);
        engineRPM = GetVelocityMagnitude() + 1000 / (gear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CalculateGearAndRMP();

        ApplyEngineForce();

        KillOrthonogalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        if(Mathf.Ceil(GetVelocityMagnitude() * 6 / maxSpeed) > gear)
        {
            StartCoroutine(ShiftGears());
        }
        if (Mathf.Ceil(GetVelocityMagnitude() * 6 / maxSpeed) < gear)
        {
            gear--;
        }
        
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        float adjustedMaxSpeed = nitroRate * maxSpeed;

        if (velocityVsUp > adjustedMaxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (velocityVsUp < -adjustedMaxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }
        if (rb.velocity.sqrMagnitude > adjustedMaxSpeed * adjustedMaxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.drag = 1;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor * nitroRate * (1f+0.5f/gear);

        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    IEnumerator ShiftGears()
    {
        if (isShifting) { 
            yield return null;
        }
        isShifting = true;
        yield return new WaitForSeconds(shiftingDelay);
        gear++;
        isShifting = false;
    }

    void ApplySteering()
    {
        float minSpeedBeforeTurning = (rb.velocity.magnitude / 8);
        minSpeedBeforeTurning = Mathf.Clamp01((minSpeedBeforeTurning));

        if (Vector2.Dot(rb.velocity, transform.up) < 0)
        {
            rotationAngle += turningInput * turningFactor * minSpeedBeforeTurning;
        }
        else
        {
            rotationAngle -= turningInput * turningFactor * minSpeedBeforeTurning;
        }
        rb.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        turningInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void KillOrthonogalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        //Vector2 righthVelocity = transform.up * Vector2.Dot(rb.velocity, transform.right);


        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, rb.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 4)
        {
            return true;
        }
        return false;
    }

    public float GetVelocityMagnitude()
    {
        return Mathf.Abs(Vector2.Dot(transform.up, rb.velocity));
    }
}
