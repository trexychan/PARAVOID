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
    private List<WaypointNode> path;

    // start point must be manually passed in through the inspector based on where Timmy spawns
    public WaypointNode startPoint;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // this is just to start things off, Timmy will stay by the nearest waypoint at spawn until A* gives him a path to get to
        path = new List<WaypointNode>();
        path.Add(startPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance == 0)
        {
            SetNextWaypoint();
        }
        // TODO: set goal to nearest waypoint to player; figure out where this should be calculated, here or in another script
        Astar(path[currWaypoint]);
    }

    private void SetNextWaypoint()
    {
        if (currWaypoint >= path.Count)
        {
            return;
        }

        navMeshAgent.SetDestination(path[currWaypoint].transform.position);
        currWaypoint++;
        if (currWaypoint >= path.Count)
        {
            currWaypoint = 0;
        }
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
                List<WaypointNode> new_path = ReconstructPath(start, curr);
                if (new_path != path)
                {
                    currWaypoint = 0;
                }
                path = new_path;
                break;
            }

            // add neighbors to open queue, ranked by cost (g + h)
            foreach (GameObject node in curr.NodeMap.Keys)
            {
                //Debug.Log(node.GetComponent<WaypointNode>());
                // run heuristic function
                float h = Heuristic(curr, node.GetComponent<WaypointNode>(), curr.NodeMap[node.gameObject]);

                node.GetComponent<WaypointNode>().ParentNode = curr;
                
                open.Enqueue(node.GetComponent<WaypointNode>(), curr.NodeMap[node.gameObject] + h);
            }

            // mark current node as visited
            closed.Add(curr);
        }
    }


    public void SetGoal(WaypointNode targ) { goal = targ; }

    bool IsGoal(WaypointNode targ) { return targ == goal; }

    /*WaypointNode FindGoal(GameObject targ)
    {
        
    }*/

    public int Heuristic(WaypointNode parent, WaypointNode node, float max_dist)
    {
        Vector3 direction = node.transform.position - parent.transform.position;

        return Physics.RaycastAll(parent.transform.position, direction, max_dist).Length;
    }

    List<WaypointNode> ReconstructPath(WaypointNode start, WaypointNode end)
    {
        List<WaypointNode> total_path = new List<WaypointNode>();
        WaypointNode curr = end;
        while (curr != start)
        {
            total_path.Add(curr);
            curr = curr.ParentNode;
        }

        return total_path;
    }
}
