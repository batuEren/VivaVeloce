using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Handles AI driving for opponent cars.
// Supports two modes: following the player directly or following a graph of waypoints.
// Includes simple car avoidance, overtaking behavior, and nitro usage on straights.

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints };

    [Header("AI Settings")]
    public AIMode mode;
    public float maxSpeed = 16f;
    public bool isAvoidingCars = true;

    public Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    Vector2 avoidenceVectorLerped = Vector2.zero;

    public WaypointNode currentWaypoint = null;
    WaypointNode previousWaypoint = null;
    WaypointNode[] allWaypoints;
    
    CarController carController;

    BoxCollider2D polyCollider2D;

    NitroAI nitro;

    public bool canMove = true;

    private bool overTakeMode = false;

    private float overTakeTimer = 0f;

    public float overTakeResetTime = 2f;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
        polyCollider2D = GetComponent<BoxCollider2D>();
        nitro = GetComponent<NitroAI>();
    }

    void Start()
    {
        previousWaypoint = currentWaypoint;
    }

    private void Update()
    {
        if(overTakeTimer > 0)
        {
            overTakeTimer -= Time.deltaTime;
        }
        else
        {
            overTakeMode = false;
        }
    }

    void FixedUpdate()
    {
        
        Vector2 inputVector = Vector2.zero;

        if (canMove)
        {
            switch (mode)
            {
                case AIMode.followPlayer:
                    FollowPlayer();
                    break;

                case AIMode.followWaypoints:
                    FollowWaypoints();
                    break;

            }

            inputVector.x = TurnTowardTarget();
            inputVector.y = ApplyThrottleOrBrake(inputVector.x);

        }

        carController.SetInputVector(inputVector);

    }

    void FollowPlayer()
    {
        if(targetTransform == null)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }   

        if(targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
    }

    void FollowWaypoints()
    {
        if (currentWaypoint == null)
        {
            currentWaypoint = FindClosestWaypoint();
            previousWaypoint = currentWaypoint;
        }
        else
        {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWaypoint = (targetPosition - transform.position).magnitude;

            if(distanceToWaypoint > 20)
            {
                Vector3 nearestPointOnTheWayPointLine = FindNearestPointOnLine(previousWaypoint.transform.position, currentWaypoint.transform.position, transform.position);

                float segments = distanceToWaypoint / 20f;

                targetPosition = (targetPosition + nearestPointOnTheWayPointLine * segments) / (segments + 1);
            }


            if (distanceToWaypoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                if (currentWaypoint.maxSpeed > 0)
                    maxSpeed = currentWaypoint.maxSpeed;
                else maxSpeed = 10000;
                
                previousWaypoint = currentWaypoint;
                if (currentWaypoint.overTakeNode != null && overTakeMode)
                {
                    currentWaypoint = currentWaypoint.overTakeNode;

                }
                else
                {
                    currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
                }
            }
        }

    }

    WaypointNode FindClosestWaypoint()
    {
        return allWaypoints.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();

    }

    float TurnTowardTarget()
    {
        Vector2 vectortoTarget = targetPosition - transform.position;
        vectortoTarget.Normalize();

        if (isAvoidingCars)
        {
            AvoidCars(vectortoTarget, out vectortoTarget);
        }
        

        float angleToTarget = Vector2.SignedAngle(transform.up, vectortoTarget);
        angleToTarget = angleToTarget * -1;

        float steerAmount = angleToTarget / 45.0f;
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }
    float ApplyThrottleOrBrake(float inputX) 
    {
        if(carController.GetVelocityMagnitude() > maxSpeed)
        {
            return 0;
        }

        if (nitro != null)
        {
            if (currentWaypoint.onStraight && inputX <= 0.5f)
            {
                nitro.usingNitro = true;
            }
            else
            {
                nitro.usingNitro = false;
            }
        }


        return 1.05f - Mathf.Abs(inputX) / 2.0f;
    }

    Vector2 FindNearestPointOnLine(Vector2 lineStart, Vector2 lineEnd, Vector2 point)
    {
        Vector2 lineHeadingVector = lineEnd - lineStart;

        float maxDistance = lineHeadingVector.magnitude;
        lineHeadingVector.Normalize();

        Vector2 lineVectorStartToPoint = point - lineStart;
        float dotProd = Vector2.Dot(lineVectorStartToPoint, lineHeadingVector);

        dotProd = Mathf.Clamp(dotProd, 0f, maxDistance);

        return lineStart + lineHeadingVector * dotProd;
    }

    bool IsCarsInfrontOfAICar(out Vector3 position, out Vector3 otherCarRightVector) 
    {
        polyCollider2D.enabled = false;
        
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * 0.5f, 1.2f, transform.up, 4, 1 << LayerMask.NameToLayer("Car"));

        polyCollider2D.enabled = true;

        if(raycastHit2D.collider != null)
        {
            Debug.DrawRay(transform.position, transform.up * 12, Color.red);

            position = raycastHit2D.collider.transform.position;
            otherCarRightVector = raycastHit2D.collider.transform.up;

            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * 12, Color.black);
            position = Vector3.zero;
            otherCarRightVector = Vector3.zero;

            return false;
        }

    }

    void AvoidCars(Vector2 vectorToTarget, out Vector2 newVectorToTarget) 
    {
        if(IsCarsInfrontOfAICar(out Vector3 otherCarPosition, out Vector3 otherCarRightVector))
        {
            Vector2 avoidenceVector = Vector2.zero;

            avoidenceVector = Vector2.Reflect((otherCarPosition - transform.position).normalized, otherCarRightVector);

            float distanceToTarget = (targetPosition - transform.position).magnitude;

            float driveToTargetInfluence = 50.0f / distanceToTarget;

            driveToTargetInfluence = Mathf.Clamp(driveToTargetInfluence, 0.3f, 1f);

            float avoidInfluence = 1.0f - driveToTargetInfluence;

            avoidenceVectorLerped = Vector2.Lerp(avoidenceVectorLerped, avoidenceVector, Time.fixedDeltaTime * 4);

            newVectorToTarget = vectorToTarget*driveToTargetInfluence + avoidenceVector*avoidInfluence;
            newVectorToTarget.Normalize();

            Debug.DrawRay(transform.position, newVectorToTarget * 4, Color.blue);

            Debug.DrawRay(transform.position, avoidenceVector * 4, Color.green);

            //OvertakeMode

            overTakeMode = true;
            overTakeTimer = overTakeResetTime;

            return;
        }
        newVectorToTarget = vectorToTarget;
    }

    public void MakeCanMove()
    {
        canMove = true;
    }

    public void MakeCanNotMove()
    {
        canMove = false;
    }
}
