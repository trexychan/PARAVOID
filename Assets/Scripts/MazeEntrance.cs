using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(player.gameObject.GetComponent<Collider>()))
        {
            timmy.SetActive(true);
            Debug.Log("Entrance Trigger Works!");
        }
    }
}
