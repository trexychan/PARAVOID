using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;

public class ApartmentScript : MonoBehaviour
{
    public GameObject dialougeText;
    public void Awake()
    {
        dialougeText = GameObject.Find("DialougeText");
        //dialougeText.GetComponent<TextProducer>().RemoveText();
        dialougeText.GetComponent<TextProducer>()
        .RunTextFor("Hello My name is bob nice to meet ya!", Effect.Type, 0.05f, 10f, true);
    }
}
