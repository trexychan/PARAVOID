using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyNum
{
    One = 0,
    Two = 1,
    Three = 2,
    Four = 3
}

public class Keys : MonoBehaviour
{
    public KeyNum num;

    public void Start()
    {
        if (GameObject.Find("Player").GetComponent<Player>().keyCollected[(int)num] == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().keys += 1;
        other.gameObject.GetComponent<Player>().keyCollected[(int)num] = true;
        Debug.Log(other.gameObject.GetComponent<Player>().keys);
        gameObject.SetActive(false);
    }
}
