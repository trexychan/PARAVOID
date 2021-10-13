using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locket : MonoBehaviour
{
    public static bool hasLocket = false;
    private void OnTriggerEnter(Collider other)
    {
        hasLocket = true;
        gameObject.SetActive(false);
    }
}
