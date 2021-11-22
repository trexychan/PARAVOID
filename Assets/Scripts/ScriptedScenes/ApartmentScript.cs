using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;

public class ApartmentScript : MonoBehaviour
{
    public GameObject dialougeText;
    public void Awake()
    {
        dialougeText = GameObject.Find("DialogueText");
        //dialougeText.GetComponent<TextProducer>().RemoveText();
        StartCoroutine(writeText());
    }

    void Update()
    {
        
    }

    private IEnumerator writeText()
    {
        yield return new WaitForSeconds(1f);
        
        dialougeText.GetComponent<TextProducer>()
        .RunTextFor("My throat is so dry...", Effect.Type, 0.04f, 4f, false);

        yield return new WaitForSeconds(4f);

        dialougeText.GetComponent<TextProducer>()
        .RunTextFor("I need a glass of water from the kitchen...", Effect.Type, 0.04f, 8f, false);
    }
}
