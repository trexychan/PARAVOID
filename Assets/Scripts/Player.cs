using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataManagement;
using ParavoidUI;

public class Player : MonoBehaviour
{
    /** Notes For this Script:
    * Note to change location of file managment storage!!!!!
    * When that is completed than check to see if the files are running properly by load changes
    * And then you should be set hopefully......
    */

    #region PlayerData
    public Vector3 currentPosition;
    public Quaternion currentRotation;
    public string currentScene;
    public DateTime dateAndTime;
    public string playerFileName;

    public byte keys;
    public bool[] keyCollected = new bool[4];
    private byte memories;
    public bool[] memoryCollected = new bool[3];
    public byte Memories
    {
        get { return memories; }

        set
        {
            memories = value;
        }
    }
    #endregion

    #region PlayerState
    private byte hp;
    public bool death = false;
    public DeathScript deathScript;
    public byte HP
    {
        get { return hp; }

        set
        {
            hp = value;
            death = hp <= 0 ? true : false;
            if (death) { deathScript.ActivateDeathPanel("You have been\nconsumed by the void."); }
        }
    }

    #endregion

    public void Awake()
    {
        gameObject.name = "Player";
        currentScene = gameObject.scene.name;
        dateAndTime = System.DateTime.Now;
        if (GameObject.Find("VisualCanvas").transform.Find("DeathPanel") != null) deathScript = GameObject.Find("VisualCanvas").transform.Find("DeathPanel").GetComponent<DeathScript>();
        HP = 3;

        //Debug.Log("Awake: " + (PlayerCarryOverData.playerDupe != null? 
        //PlayerCarryOverData.playerDupe.playerFileName : "Null"));

        if (PlayerCarryOverData.playerDupe != null)
        {
            //Debug.Log("It Ran");
            PastePlayerData(PlayerCarryOverData.playerDupe);
            PlayerCarryOverData.playerDupe = null;
        }

        //Go ahead and make sure that when it loads the player, this object will carry over as a copy with a name change and the other will be fond to set the information to....
    }

    public void Update()
    {
        currentPosition = transform.localPosition; //Intended to track the player position
        currentRotation = transform.localRotation;

        if (currentScene != gameObject.scene.name
            && gameObject.scene.name != "DontDestroyOnLoad")
        {
            currentScene = gameObject.scene.name;
        }
    }

    #region Player Save System Methods

    //This will be the default values for any new game that is started
    public void NewPlayer(string slotName)
    {
        currentScene = "ApartmentLevelMaster";
        currentPosition = new Vector3(-0.6234398F, 0.9919997F, 0.7313685F);
        currentRotation = transform.localRotation;

        this.SavePlayer(slotName);
        this.LoadPlayer(slotName);
    }

    public void SavePlayer(string slotName)
    {
        dateAndTime = System.DateTime.Now;
        playerFileName = slotName;

        SaveSystem.SerializePlayerData(this, slotName);
    }

    public void LoadPlayer(string slotName)
    {
        PlayerData data = SaveSystem.DeserializePlayerData(slotName);

        //Load Data
        currentScene = data.currentScene;
        Debug.Log(currentScene);

        //Load File Data
        dateAndTime = data.dateAndTime;
        playerFileName = data.playerFileName;

        //Level Specific
        keys = data.keys;
        Memories = data.memories;

        keyCollected = data.keyCollected;
        memoryCollected = data.memoryCollected;

        //Load specific objects, entities, and player positions and states
        transform.localPosition = new Vector3(data.currentPosition[0], data.currentPosition[1], data.currentPosition[2]);
        Debug.Log(data.currentRotation[0] + ", " + data.currentRotation[1] + ", " + data.currentRotation[2] + ", " + data.currentRotation[3] + ", ");
        transform.localRotation = new Quaternion(data.currentRotation[0], data.currentRotation[1], data.currentRotation[2], data.currentRotation[3]);

        //Load the scene

        //Debug.Log("Loader: " + (PlayerCarryOverData.playerDupe != null? 
        //PlayerCarryOverData.playerDupe.playerFileName : "Null"));
        SceneLoader.LoadScene(currentScene);
    }

    public void PastePlayerData(Player player)
    {
        try
        {
            PlayerData data = SaveSystem.DeserializePlayerData(player.playerFileName);

            //Load Data
            currentScene = data.currentScene;

            //Level Specific
            keys = data.keys;
            Memories = data.memories;

            keyCollected = data.keyCollected;
            memoryCollected = data.memoryCollected;

            //Load File Data
            dateAndTime = data.dateAndTime;
            //playerFileName = data.playerFileName;

            //Load specific objects, entities, and player positions and states
            transform.position = new Vector3(data.currentPosition[0], data.currentPosition[1], data.currentPosition[2]);

            transform.rotation = new Quaternion(data.currentRotation[0], data.currentRotation[1], data.currentRotation[2], data.currentRotation[3]);
        }
        catch (NullReferenceException ex)
        {
            //Load Data
            currentScene = player.currentScene;

            //Level Specific
            keys = player.keys;
            Memories = player.Memories;

            keyCollected = player.keyCollected;
            memoryCollected = player.memoryCollected;

            //Load File Data
            dateAndTime = player.dateAndTime;
            //playerFileName = player.playerFileName;
        }
    }

    public void ErasePlayer(string slotName)
    {
        SaveSystem.SerializePlayerData(null, slotName);
    }

    public void DeletePlayer(string slotName)
    {
        SaveSystem.DeleteFile(slotName);
        //Destroy(saveContent.transform.Find("SaveSlot (" + slotName + ")").gameObject); --Make a call to SlotManager
    }

    #endregion
}