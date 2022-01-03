using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeEntrance : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] GameObject timmy;
    // Start is called before the first frame update
    void Start()
    {
        timmy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timmy.activeInHierarchy)
        {
            NavMeshAgent navMesh = timmy.GetComponent<NavMeshAgent>();
            navMesh.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(player.gameObject.GetComponent<Collider>()))
        {
            timmy.SetActive(true);
            Debug.Log("Entrance Trigger Works!");
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
