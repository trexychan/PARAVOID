using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioSource sound;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = enemy.gameObject.transform.position
            - player.gameObject.transform.position;
        if (direction.magnitude < 40)
        {
            sound.volume = 1 - (direction.magnitude / 40);
            sound.pitch = 3 * (1 - (direction.magnitude / 40));
        }
        else
        {
            sound.volume = 0;
        }
    }
}
