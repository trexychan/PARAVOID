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

    private IEnumerator writeText()
    {
        dialougeText.GetComponent<TextProducer>()
        .RunText("Hello My name is bob nice to meet ya!", Effect.Type, 0.05f);

        yield return new WaitForSeconds(3f);

        dialougeText.GetComponent<TextProducer>()
        .RunTextFor("Hello My name is bob nice to meet ya!", Effect.Type, 0.05f, 10f, true);
    }
}
