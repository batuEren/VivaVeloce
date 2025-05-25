using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePathDrawer : MonoBehaviour
{
    public WaypointNode firstNode;
    void OnDrawGizmos()
    {
        if(firstNode == null)
        {
            return;
        }

        DrawNode(firstNode);
    }

    public void DrawNode(WaypointNode node)
    {
        WaypointNode[] childeren = node.nextWaypointNode;

        foreach(WaypointNode child in childeren)
        {
            Gizmos.DrawCube(child.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
            
            if(child == null)
            {
                continue;
            }
            Gizmos.DrawLine(node.transform.position, child.transform.position);
            if(child == firstNode)
            {
                continue;
            }

            if(node.overTakeNode != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(node.transform.position, node.overTakeNode.transform.position);
                Gizmos.DrawCube(node.overTakeNode.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
                Gizmos.color = Color.white;

                DrawNode(node.overTakeNode);
            }

            DrawNode(child);
        }
    }


}
