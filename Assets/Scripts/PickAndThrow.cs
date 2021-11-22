using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndThrow : MonoBehaviour
{
    public GameObject item;
    private bool pickedUp;
    public float pickupDistance = 2f;
    public float holdDistance = .5f;
    public float throwPower = 50f;
    

    void Awake() {
        pickedUp = false;
    }

    void Update() 
    {
        RaycastHit hit;
        if (!pickedUp && Physics.Raycast(transform.position, transform.forward, out hit, pickupDistance))  // pick up if looking at
        {
            if (Input.GetButtonDown("Fire1"))
            {
                pickedUp = true;
            }
        }

        if (pickedUp)
        {
            item.GetComponent<BoxCollider>().enabled = false;
            item.transform.position = transform.position + holdDistance * transform.forward;
        }

        if (Input.GetButtonDown("Fire2") && pickedUp)
        {
            pickedUp = false;
            item.GetComponent<BoxCollider>().enabled = true;
            if (item.GetComponent<BoxCollider>().enabled) {
                item.GetComponent<Rigidbody>().AddForce(throwPower * transform.forward, ForceMode.VelocityChange);
            }
        }
        
    }

}