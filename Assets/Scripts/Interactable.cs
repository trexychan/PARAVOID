using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public GameObject promptText;
    public string dest_scene;
    public Vector3 dest_pos;
    PlayerController player;
    private bool playerInRange = false;
    
    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
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
        promptText.SetActive(true);
        playerInRange = true;
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
        if (playerInRange && player.playerControls.Player.Interact.triggered)
        {
            SwitchSite();
        }
    }
}
