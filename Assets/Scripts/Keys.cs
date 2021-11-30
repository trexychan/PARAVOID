using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public AudioManager a;
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        other.gameObject.GetComponent<Player>().keys += 1;
        Debug.Log(other.gameObject.GetComponent<Player>().keys);
        a.Play("KeyPickup");
    }
}
