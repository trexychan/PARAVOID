using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Paravoid.DataStructures;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public GameObject waypoints;
    private WaypointNode goal;
    private int currWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currWaypoint = -1;
        SetNextWaypoint();
        foreach (Transform waypoint in waypoints.transform)
        {
            Debug.Log($"Timmy's waypoint: {waypoint.gameObject.name}");
        }
        // these is important
        //navMeshAgent.stoppingDistance
        //navMeshAgent.SetDestination()
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance == 0)
        {
            SetNextWaypoint();
        }
    }

    private void SetNextWaypoint()
    {
        if (waypoints.transform.childCount == 0)
        {
            Debug.LogWarning("No waypoints found");
        }

        if (currWaypoint >= waypoints.transform.childCount)
        {
            currWaypoint = -1;
        }
        else
        {
            currWaypoint++;
        }

        navMeshAgent.SetDestination(waypoints.GetComponentInChildren<WaypointNode>().transform.position);
    }

    void Astar(WaypointNode start)
    {
        HashSet<WaypointNode> closed = new HashSet<WaypointNode>(); // Start CLOSED as empty set

        PriorityQueue<WaypointNode, float> open = new PriorityQueue<WaypointNode, float>(); // OPEN, PQ containing START
        open.Enqueue(start, 0);

        WaypointNode curr = start;

        while (open.Count > 0) // while lowest rank in OPEN is not the GOAL
        {
            curr = open.Dequeue();

            if (IsGoal(curr))
            {
                // TODO: need to set goal parent here for reconstruction
                break;
            }

            // add neighbors to open queue, ranked by cost (g + h)
            foreach (GameObject node in curr.NodeMap.Keys)
            {
                Debug.Log(node.GetComponent<WaypointNode>());
                // run heuristic function
                //float h = Heuristic(curr, node.GetComponent<WaypointNode>());

                // TODO: need to set node parent here for reconstruction

                //open.Enqueue(node.GetComponent<WaypointNode>(), curr.NodeMap[node.gameObject] + h);
            }

            // mark current node as visited
            closed.Add(curr);
            //curr = open.Dequeue();
        }

        // reconstruct solution
        // double check how to do this fo rmoving goal
        ReconstructPath(closed, curr);

    }

    /// <summary>
    /// Implements the A* Pathfinding algorithm to determine a path from the monster to the player.
    /// 
    /// Based on pseudocode explanation from "Amit's Thoughts on Pathfinding":
    /// http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html
    /// </summary>
    /// <param name="start">Starting node in the algorithm</param>

    // void AStar2(WaypointNode start)
    // {
    //     PriorityQueue<WaypointNode, float> open = new PriorityQueue<WaypointNode, float>();
    //     open.Enqueue(start, 0);
    //     HashSet<WaypointNode> closed = new HashSet<WaypointNode>();
    //     Stack<WaypointNode> reconstructedPathStack = new Stack<WaypointNode>();
    //     while (!open.GetMinPriorityElement().Equals(goal)) // while lowest rank in OPEN is not the GOAL
    //     {
    //         WaypointNode current = open.Dequeue();
    //         closed.Add(current);
    //         foreach (GameObject neighbor in current.NodeMap.Keys)
    //         {
    //             float cost = Heuristic(current);

    //             // if neighbor in OPEN and cost less than g(neighbor)
    //             if (open.Contains(neighbor.GetComponent<WaypointNode>()) && cost < current.NodeMap[neighbor])
    //             {
    //                 // Remove neighbor from OPEN, because new path is better
    //                 open.Remove(neighbor.GetComponent<WaypointNode>());
    //             }

    //             // if neighbor in CLOSED and cost less than g(neighbor)
    //             if (closed.Contains(neighbor.GetComponent<WaypointNode>()) && cost < current.NodeMap[neighbor])
    //             {
    //                 // remove neighbor from closed
    //                 closed.Remove(neighbor.GetComponent<WaypointNode>());
    //             }
    //             if (!open.Contains(neighbor.GetComponent<WaypointNode>()) && !closed.Contains(neighbor.GetComponent<WaypointNode>()))
    //             {
    //                 // set g(neighbor) to cost
    //                 current.NodeMap.Add(neighbor, cost);
    //                 // add neighbor to OPEN
    //                 // set priority queue rank to g(neighbor) + h(neighbor)
    //                 float rank = current.NodeMap[neighbor]; // g(neighbor)
    //                 open.Enqueue(neighbor.GetComponent<WaypointNode>(), rank + Heuristic(neighbor.GetComponent<WaypointNode>()));
    //                 // set neighbor's parent to current (confused about this part tbh)
    //                 neighbor.GetComponent<WaypointNode>().ParentNode = current;
    //             }
    //         }
    //     }
    //     // reconstruct reverse path from goal to start by following parent pointers
    //     WaypointNode reversePathNode = goal;
    //     while (reversePathNode != null)
    //     {
    //         reconstructedPathStack.Push(reversePathNode);
    //         reversePathNode = reversePathNode.ParentNode;
    //     }
    // }


    void SetGoal(WaypointNode targ) { goal = targ; }

    bool IsGoal(WaypointNode targ) { return targ == goal; }

    public int Heuristic(WaypointNode parent, WaypointNode node)
    {
        //Vector3 direction = child.transform.position - parent.transform.position;
        Vector3 direction = Vector3.zero;

        // TODO: Cap raycast length at node, otherwise will get walls past target
        return Physics.RaycastAll(parent.transform.position, direction).Length;
    }

    void ReconstructPath(HashSet<WaypointNode> cameFrom, WaypointNode curr)
    {
        WaypointNode totalPath = curr;
        while (cameFrom.Contains(curr))
        {

        }
    }
}
