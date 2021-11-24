using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateKeys : MonoBehaviour
{
    public Player player;
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        //textBox = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = "Keys: " + player.keys.ToString();
    }
}
