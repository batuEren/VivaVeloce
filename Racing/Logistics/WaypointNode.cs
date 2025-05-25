using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    public float maxSpeed = 0f;

    public bool onStraight = false;
    
    public float minDistanceToReachWaypoint = 1f;
    
    public WaypointNode[] nextWaypointNode;

    public WaypointNode overTakeNode;
}
