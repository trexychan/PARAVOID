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

    [SerializeField] private bool isTriggered;
    [SerializeField] private Pathfinding pf;

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
            }
        }

    }

    /// <summary>
    /// Activated when a collider enters the trigger.
    /// </summary>
    /// <param name="other">Collider that enters the trigger.</param>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Debug.Log($"{collider.gameObject.name} has collided with {this.gameObject.name}");
            pf.SetGoal(this);
            isTriggered = true;
        }
    }

    /// <summary>
    /// Activated when a collider stays in the trigger and does not leave.
    /// </summary>
    /// <param name="collider">Collider that stays in the trigger.</param>
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isTriggered = true;
        }
    }

    /// <summary>
    /// Activated when a collider leaves the trigger.
    /// </summary>
    /// <param name="collider">Collider that leaves the trigger.</param>
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Debug.Log($"{collider.gameObject.name} has exited from collider {this.gameObject.name}");
            //parent.GetComponent<>().SetGoal(this);
            isTriggered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //DidRaycastHitPlayer();
    }
}
