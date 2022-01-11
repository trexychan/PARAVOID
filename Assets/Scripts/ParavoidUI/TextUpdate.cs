using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Slider targetedRead;
    public Text textObject;

    public void Start()
    {
        textObject = GetComponent<Text>();
    }

    public void Update()
    {
        textObject.text = (int)(targetedRead.value * 100) + "";
    }
}
