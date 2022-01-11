using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("WakeUp");
    }

    // Update is called once per frame
    
}
