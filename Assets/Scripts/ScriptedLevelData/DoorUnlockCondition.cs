using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;

public class DoorUnlockCondition : MonoBehaviour
{
    public ClockCounter clock;
    public Player player;
    public bool clockMode; //if false, then condition is for keys instead
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        clock = GameObject.Find("VisualCanvas").transform.Find("IngameUIPanel").Find("Clock").GetComponent<ClockCounter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if ((clockMode && clock.timeLeft <= 0) || (!clockMode && player.keys >= 4))
        {
            SceneLoader.LoadScene("mall"); //Please put an actual scene in there
            gameObject.SetActive(false);
        }
    }
}
