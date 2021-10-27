using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public GameObject[] waypoints;
    int currWaypoint;


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
}
