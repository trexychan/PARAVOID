using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider trigger)
    {
        GameObject item = trigger.gameObject;
        if (item.tag.Equals("Collectible"))
        {
            item.SetActive(false);
            inventory.Add(item);
        }

    }
}
