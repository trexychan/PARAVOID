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

    #endregion

    #region ExternalData
    
    //For Settings Panel Preferences
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;

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

    public void SavePlayer(string slotName) //
    {
        if(!files.Contains(slotName))
        {
            dateAndTime = System.DateTime.Now.ToString();

            SaveSystem.SerializePlayerData(this, slotName);

            //addNewSlot(slotName);

            files.Add(slotName);
        }
        else
        {
            dateAndTime = System.DateTime.Now.ToString();
            SaveSystem.SerializePlayerData(this, slotName);

            //saveContent.transform.Find("SaveSlot (" + slotName + ")").transform.Find("Text").GetComponent<Text>().text =
            //"FILE: " + slotName +
            //"\nLAST PLAYED: " + dateAndTime;
        }
    }

    public void LoadPlayer(string slotName)
    {
        PlayerData data = SaveSystem.DeserializePlayerData(slotName);

        //Load Data
        currentScene = data.currentScene;
        dateAndTime = data.dateAndTime;

        //Load specific objects, entities, and player positions and states
        transform.position = new Vector3(data.currentPosition[0], data.currentPosition[1], data.currentPosition[2]);
    }

    public void DeletePlayer(string slotName)
    {
        SaveSystem.DeleteFile(slotName);
        files.Remove(slotName);
        //Destroy(saveContent.transform.Find("SaveSlot (" + slotName + ")").gameObject);

        //SaveGameFiles();
    }

    #endregion
}
