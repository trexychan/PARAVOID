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
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance == 0)
        {
            setNextWaypoint();
        }
    }

    private void setNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints found");
        }

        if (currWaypoint >= waypoints.Length)
        {
            currWaypoint = -1;
        }
        else
        {
            currWaypoint++;
        }

        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    void Astar(WaypointNode start)
    {
        HashSet<WaypointNode> closed = new HashSet<WaypointNode>();
        PriorityQueue<WaypointNode, float> open = new PriorityQueue<WaypointNode, float>();
        open.Enqueue(start, 0);
        WaypointNode curr = start;

        while (!isGoal(curr) && open.Count > 0)
        {
            // mark current node as visited
            closed.Add(current);

            // add neighbors to open queue, ranked by cost (g + h)
            foreach (GameObject node in curr.NodeMap.Keys)
            {
                // run heuristic function
                float h = heuristic(node);
                open.Enqueue(node, curr.NodeMap[node.gameObject] + h);
            }

            open.Dequeue();

            curr = open.Dequeue();
        }

        if (isGoal(curr))
        {
            //reconstruct solution
            // double check how to do this fo rmoving goal
        }
        else
        {
            // switch back to random nav
        }

    }

    void setGoal(WaypointNode targ) { goal = targ; }

    bool isGoal(WaypointNode targ) { return targ == goal; }

    float heuristic(WaypointNode node)
    {
        return 0f;
    }
}
