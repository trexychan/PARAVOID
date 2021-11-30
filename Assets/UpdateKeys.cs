using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateKeys : MonoBehaviour
{
    public Player player;
    public Text textBox;
    public Sprite KeyFilled;
    public bool graphicMode;

    private CanvasGroup canvasFade;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        canvasFade = GetComponent<CanvasGroup>();
        canvasFade.alpha = 0f;
        //textBox = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!graphicMode)
            textBox.text = "Keys: " + player.keys.ToString();


        if (graphicMode)
        {

            int i = 0;
            foreach (Transform child in transform)
            {
                if (i >= player.keys) { break; }
                canvasFade.alpha = 1f;
                child.GetComponent<Image>().sprite = KeyFilled;
                i++;
            }
        }
    }
}
