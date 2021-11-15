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
        if (waypoints.GetComponentsInChildren<GameObject>().Length == 0)
        {
            Debug.LogWarning("No waypoints found");
        }

        if (currWaypoint >= waypoints.GetComponentsInChildren<GameObject>().Length)
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
        HashSet<WaypointNode> closed = new HashSet<WaypointNode>();
        PriorityQueue<WaypointNode, float> open = new PriorityQueue<WaypointNode, float>();
        open.Enqueue(start, 0);
        WaypointNode curr = start;

        while (!IsGoal(curr) && open.Count > 0)
        {
            // mark current node as visited
            closed.Add(curr);

            // add neighbors to open queue, ranked by cost (g + h)
            foreach (GameObject node in curr.NodeMap.Keys)
            {
                // run heuristic function
                float h = Heuristic(node.GetComponent<WaypointNode>());
                open.Enqueue(node.GetComponent<WaypointNode>(), curr.NodeMap[node.gameObject] + h);
            }

            open.Dequeue();

            curr = open.Dequeue();
        }

        if (IsGoal(curr))
        {
            //reconstruct solution
            // double check how to do this fo rmoving goal
        }
        else
        {
            // switch back to random nav
        }

    }

    void SetGoal(WaypointNode targ) { goal = targ; }

    bool IsGoal(WaypointNode targ) { return targ == goal; }

    float Heuristic(WaypointNode node)
    {
        return 0f;
    }
}
