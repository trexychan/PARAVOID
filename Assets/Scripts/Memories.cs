using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memories : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        other.gameObject.GetComponent<Player>().Memories += 1;
        Debug.Log(other.gameObject.GetComponent<Player>().Memories);
    }
}
