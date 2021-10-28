using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehaviour : MonoBehaviour
{

    [SerializeField] GameObject waypoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        SetWaypointGValues();
    }

    private void SetWaypointGValues()
    {
        foreach (Transform waypoint in transform)
        {
            GameObject child = waypoint.gameObject;
            //Debug.Log(child.name);
            foreach (Transform waypointTarget in transform)
            {
                if (waypoint.Equals(waypointTarget))
                {
                    continue;
                }
                else
                {
                    //if (Physics.Raycast())
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
