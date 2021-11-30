using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ParavoidUI;
using UnityEngine.SceneManagement;
using Fragsurf.Movement;

public class Interactable : MonoBehaviour
{
    public GameObject promptText;
    public string dest_scene;
    public Vector3 dest_pos;
    private PlayerController player;
    private TextProducer dialogue_text;
    private bool playerInRange = false;

    void Awake()
    {
        player = PlayerController.singleton;
        promptText = GameObject.Find("VisualCanvas").transform.Find("IngameUIPanel").Find("InteractText").gameObject;
        Debug.Log(promptText.name);
        dialogue_text = GameObject.Find("DialogueText").GetComponent<TextProducer>();
        
    }

    void Start()
    {
        if (promptText)
        {
            promptText.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (gameObject.CompareTag("SceneTransition"))
        {
            SwitchSite();
        }
        else
        {
            promptText.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        promptText.SetActive(false);
        playerInRange = false;
    }

    public void SwitchSite()
    {
        promptText.SetActive(false);
        SceneManager.LoadScene(dest_scene);
    }

    void Update()
    {
        if (playerInRange && player.playerControls.Player.Interact.triggered)
        {
            switch (gameObject.tag)
            {
                case "InteractableSceneTransition":
                    SwitchSite();
                    break;
                case "Cup":
                    CupInteractEvent();
                    break;
                case "locket":
                    LocketInteractEvent();
                    break;
            }
        }
    }

    private void CupInteractEvent()
    {
        // picked up cup!
            dialogue_text.RunTextFor("It's late... I should go to bed.", Effect.Type, 0.04f, 8f, false);
            //change scenetransition to broken apartment
            GameObject doortransition = GameObject.Find("BedroomDoor").transform.Find("DoorTransition").gameObject;
            Debug.Log(doortransition);
            doortransition.SetActive(true);
            promptText.SetActive(false);

            gameObject.SetActive(false);
    }

    private void LocketInteractEvent()
    {
        GameObject[] idleMonsterProp = GameObject.FindGameObjectsWithTag("MonsterProp");
        foreach (GameObject destructible in idleMonsterProp)
        {
            destructible.SetActive(false);
        }
        player.doubleJump = true;
        promptText.SetActive(false);
        gameObject.SetActive(false);
    }

}
