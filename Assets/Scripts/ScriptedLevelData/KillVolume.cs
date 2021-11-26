using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<Player>().HP = 0;
    }
}
