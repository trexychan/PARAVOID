using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to create the characteristics of the Waypoint Node for
/// Timmy's Pathfinding Algorithm (A*).
/// 
/// The Node has the following properties:
///     nodeList: List of nodes connected to it
///     G: Path cost between nodes
///     H: Heuristic value(s) (Which Timmy will calculate) (TBD)
///     F: G + H (TBD)
///     
/// 
/// </summary>
public class WaypointNode : MonoBehaviour
{

    [SerializeField] GameObject parent;

    // parent node
    private WaypointNode parentNode;

    /// <summary>
    /// Stores the parent node of this node. Useful in reverse reconstruction.
    /// </summary>
    public WaypointNode ParentNode
    {
        get
        {
            return parentNode;
        }
        set
        {
            parentNode = value;
        }
    }

    /**
     * Store legal nodes for the path with their G-Values as a Dictionary
     */
    private Dictionary<GameObject, float> _nodeMap;

    /// <summary>
    /// Access the NodeMap as a C# Property. Read-only outside of this
    /// class.
    /// </summary>
    public Dictionary<GameObject, float> NodeMap
    {
        get
        {
            return _nodeMap;
        }
        private set
        {
            _nodeMap = value;
        }
    }

    
    private void Awake()
    {
        NodeMap = new Dictionary<GameObject, float>();
        ConnectNodes();
        Debug.Log($"Current Node: {this.gameObject.name}");
        foreach (GameObject node in NodeMap.Keys)
        {
            Debug.Log($"Waypoint: {node.name}\nG Value: {NodeMap[node]}");
        }
    }

    /// <summary>
    /// For each node, I want to raycast to another waypoint in
    /// the maze, and if there is no collider/wall blocking the
    /// raycast, add it to the NodeList.
    /// </summary>
    private void ConnectNodes()
    {
        //Transform parent = GetComponentInParent<GameObject>().transform;
        foreach (Transform child in parent.transform)
        {
            
            if (!child.gameObject.Equals(this.gameObject))
            {
                // If raycast doesn't hit something between waypoints, add Node to list
                if (!Physics.Raycast(this.gameObject.transform.position,                        // Start position of raycast
                    (child.position - this.gameObject.transform.position),                      // Direction of raycast
                    (child.position - this.gameObject.transform.position).magnitude - 1f        // Max distance of raycast
                    ))  
                {
                    Debug.DrawRay(this.gameObject.transform.position,
                        child.position - this.gameObject.transform.position,
                        color: Color.green,
                        duration: Mathf.Infinity);
                    NodeMap.Add(child.gameObject, (child.position - this.gameObject.transform.position).magnitude);
                }
                //else
                //{
                //    Debug.DrawRay(this.gameObject.transform.position,
                //        child.position - this.gameObject.transform.position,
                //        color: Color.red,
                //        duration: Mathf.Infinity);
                //}
            }
        }

    }

    /// <summary>
    /// Uses a raycast to detect if the player is within the trigger distance
    /// for a node, defined as within halfway between that node and the next
    /// node that it is connected to.
    /// </summary>
    /// <returns>Whether the player was hit by a raycast</returns>
    //private bool DidRaycastHitPlayer()
    //{
    //    /**
    //     * There's two problems I can think of with this approach:
    //     *   1) The literal edge cases (i.e. the player literally being on the edges of the maze path/on the wall
    //     *      Possible solution: make the nodes themselves larger in size s.t. they can't really ignore the player
    //     *   // 2) What if the raycast hits the monster? How do we make sure the wait a minute...
    //     *   Scrap number 2, the monster already has ignore raycast tag on it.
    //     */
    //    GameObject player = GameObject.FindWithTag("Player");
    //    foreach (GameObject waypoint in NodeMap.Keys)
    //    {
    //        if (Physics.Raycast(this.gameObject.transform.position,                                             // Start position of raycast
    //                (waypoint.transform.position - this.gameObject.transform.position),                         // Direction of raycast
    //                ((waypoint.transform.position - this.gameObject.transform.position).magnitude - 1f) / 2     // Max distance of raycast
    //                ))
    //        {
    //            Debug.LogWarning($"Player is within trigger distance of {this.gameObject.name}");
    //            Debug.DrawLine(player.transform.position, this.gameObject.transform.position, Color.magenta, Time.deltaTime * 5);
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    /// <summary>
    /// Activated when a collider enters the trigger.
    /// </summary>
    /// <param name="other">Collider that enters the trigger.</param>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Debug.Log($"{this.gameObject.name} has collided with {collider.gameObject.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //DidRaycastHitPlayer();
    }
}
