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

    //private int _g;

    //public int G
    //{
    //    get
    //    {
    //        return g;
    //    }
    //    set
    //    {
    //        _g = value;
    //    }
    //}

    /**
     * Including this as a list in the WaypointNode class rather
     * than in WaypointBehaviour class should make things easier
     * to read and more organized in general. Otherwise we're
     * iterating through every node 
     */
    private ArrayList _nodeList;

    public ArrayList NodeList
    {
        get
        {
            return _nodeList;
        }
        private set
        {
            _nodeList = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        NodeList = new ArrayList();
        ConnectNodes();
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
                if (!Physics.Raycast(this.gameObject.transform.position,     // Start position of raycast
                    (child.position - this.gameObject.transform.position),  // Direction of raycast
                    80f))                                                   // Max distance of raycast
                {
                    Debug.DrawRay(this.gameObject.transform.position,
                        child.position - this.gameObject.transform.position,
                        color: Color.green,
                        duration: Mathf.Infinity);
                    NodeList.Add(child.gameObject);
                }
                else
                {
                    Debug.DrawRay(this.gameObject.transform.position,
                        child.position - this.gameObject.transform.position,
                        color: Color.red,
                        duration: Mathf.Infinity);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
