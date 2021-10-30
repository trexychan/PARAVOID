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
    SurfCharacter player;
    private bool playerInRange = false;

    void Awake()
    {
        player = FindObjectOfType<SurfCharacter>();
        promptText = GameObject.Find("VisualCanvas").transform.Find("IngameUIPanel").Find("InteractText").gameObject;
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
        SceneManager.LoadScene(dest_scene);
    }

    void Update()
    {
        if (playerInRange && player.InteractPressed && gameObject.CompareTag("InteractableSceneTransition"))
        {
            SwitchSite();
        }
        else if (playerInRange && player.InteractPressed && gameObject.CompareTag("Cup"))
        {
            // picked up cup!
            GameObject.Find("DialougeText").GetComponent<TextProducer>()
            .RunTextFor("It's late... I should go to bed.", Effect.Type, 0.04f, 8f, false);
            //change scenetransition to broken apartment

            promptText.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
