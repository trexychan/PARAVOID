using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using DataManagement;

public class Player : MonoBehaviour 
{
    #region PlayerData

    public Vector3 currentPosition;
    public Scene currentScene;
    public string dateAndTime;
    public string playerFileName;

    #endregion

    #region UniversalData

    //For Settings General Panel Preferences

    //For Settings Graphics Panel Preferences
      
    //For Settings Audio Panel Preferences
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;

    //For Settings Controls Panel Preferences

    //For Save Panel Data
    public List<string> files = new List<string>();

    #endregion

    public void Awake() 
    {
        currentScene = gameObject.scene;
        dateAndTime = System.DateTime.Now.ToString();
    }

    public void Update() 
    {
        currentPosition = transform.position; //Intended to track the player position

        if (currentScene != gameObject.scene) 
        {
            currentScene = gameObject.scene;
        }
    }

    #region Player Save System Methods

    public void SavePlayer(string slotName)
    {
        dateAndTime = System.DateTime.Now.ToString();
        playerFileName = slotName;

        SaveSystem.SerializePlayerData(this, slotName);

        if(!files.Contains(slotName))
        {
            files.Add(slotName);

            SaveGameFiles();
        }
    }

    public void LoadPlayer(string slotName)
    {
        PlayerData data = SaveSystem.DeserializePlayerData(slotName);

        //Load Data
        currentScene = data.currentScene;

        //Load File Data
        dateAndTime = data.dateAndTime;
        playerFileName = data.playerFileName;

        //Load specific objects, entities, and player positions and states
        transform.position = new Vector3(data.currentPosition[0], data.currentPosition[1], data.currentPosition[2]);
    }

    public void ErasePlayer(string slotName)
    {
        SaveSystem.SerializePlayerData(null, slotName);
    }

    public void DeletePlayer(string slotName)
    {
        SaveSystem.DeleteFile(slotName);
        files.Remove(slotName);
        //Destroy(saveContent.transform.Find("SaveSlot (" + slotName + ")").gameObject); --Make a call to SlotManager

        SaveGameFiles();
    }

    #endregion

    #region GameFiles

    public void SaveGameFiles()
    {
        SaveSystem.SerializeGameFiles(this);
    }

    public void LoadGameFiles()
    {   
        UniversalData data = SaveSystem.DeserializeGameFiles();

        if (data == null)
        {
            Debug.Log("Adding Universal Data");
            SaveGameFiles();
            data = SaveSystem.DeserializeGameFiles();
        }

        files = data.files;

        //--make a call to slotmanager to re-add all slots

        masterVolume = data.masterVolume;
        musicVolume = data.musicVolume;
        SFXVolume = data.SFXVolume;
    }

    public bool doesFileExist(string slotName)
    {
        foreach (string file in files)
            if (file == slotName)
                return true;
        
        return false;
    }

    #endregion
}
