using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;

public class DoorUnlockCondition : MonoBehaviour
{
    public ClockCounter clock;
    public Player player;
    public PlayerController playerController;
    public bool clockMode; //if false, then condition is for keys instead
    private GameObject promptText;
    private bool playerInRange = false;

    [SerializeField] private bool isTimmyLevel;
    [SerializeField] private GameObject timmy;
    private bool isGarageTriggered = false;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerController = player.gameObject.GetComponent<PlayerController>();
        promptText = GameObject.Find("VisualCanvas").transform.Find("IngameUIPanel").Find("InteractText").gameObject;
        clock = GameObject.Find("VisualCanvas").transform.Find("IngameUIPanel").Find("Clock").GetComponent<ClockCounter>();
        clock.clockStart = false;
        if (isTimmyLevel && timmy != null)
        {
            timmy.SetActive(false);
            this.GetComponent<Interactable>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!clockMode && playerInRange)
        {
            if (playerController.playerControls.Player.Interact.triggered && player.keys < 4)
            {
                GameObject.Find("DialogueText").GetComponent<TextProducer>().ReplaceTextFor(
                                    "Need " + (4 - player.keys).ToString() + " key" + (player.keys != 3 ? "s" : "") + " to open", Effect.Type, 0.04f, 4f, true);
            } else if (playerController.playerControls.Player.Interact.triggered && player.keys >= 4)
            {
                SceneLoader.LoadScene("ParkingMaster");
            }
        }
        if (clock.clockStart)
        {
            clock.timeLeft -= Time.deltaTime;
            Debug.Log($"Time Left: {clock.timeLeft}");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        playerInRange = true;
        promptText.SetActive(true);
        
        if ((clockMode && clock.timeLeft <= 0) || (!clockMode && player.keys >= 4))
        {
            //SceneLoader.LoadScene("ApartmentLevelMaster"); //Please put an actual scene in there
            //gameObject.SetActive(false);
            this.GetComponent<Interactable>().enabled = true;
        }
        else if (clockMode)
        {
            GameObject.Find("DialogueText").GetComponent<TextProducer>().ReplaceTextFor(
                    "Elevator hasn't arrived", Effect.Type, 0.04f, 4f, true);
            if (isTimmyLevel)
            {
                timmy.SetActive(true);
                clock.clockStart = true;
                if (!isGarageTriggered)
                {
                    clock.timeLeft = 80f;
                }
                isGarageTriggered = true;
            }
        }
        else if (!clockMode)
        {
            
        }

    }

    public void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        promptText.SetActive(false);
    }
}
