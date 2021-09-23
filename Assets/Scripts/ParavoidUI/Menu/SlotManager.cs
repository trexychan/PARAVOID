using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

namespace ParavoidUI
{
    public class SlotManager : MonoBehaviour
    {
        public GameObject saveSlotPrefab;
        public GameObject addNewSlotPrefab;
        public Player player;
        public SelectionManagement selectManager;

        //windows
        public GameObject createNewFileWindow;
        public GameObject alertWindow;

        public GameObject targetSlot;
        public List<GameObject> saveSlots;

        public void Awake()
        {
            selectManager = gameObject.GetComponent<SelectionManagement>();
            //Auto assigns player if their exisists a player GameObject in scene
            try
            {
                player = GameObject.Find("Player").GetComponent<Player>(); 

                player.LoadGameFiles();
                AddAllSavedSlots(player.files);
            }
            catch (NullReferenceException ex)
            {
                Debug.LogError("Error: SlotManager's player variable has not been assigned a value \n[No GameObject nammed \"Player\" found to auto assign variable]"); 
            }             
                        
        }

        public void Update()
        {
            foreach (GameObject slot in saveSlots)
                if(slot.GetComponent<Toggle>().isOn)
                    targetSlot = slot;
        }

        

        #region SlotManger methods

        public void AddNewSaveFile(string slotName)
        {
            //Checks if saveslot name already exsists
            foreach (GameObject slot in saveSlots)
                if(slot.GetComponent<SaveSlot>().slotName == slotName)
                    return;

            player.SavePlayer(slotName);
            //Add Environment Data and other game specific stuff
            AddNewSlot(slotName);
        }

        public void AddNewSlot(string slotName)
        {
            PlayerData data = SaveSystem.DeserializePlayerData(slotName);

            //saveSlot
            GameObject newSlot = Instantiate(saveSlotPrefab);
            newSlot.name = FormatSlotInspectorName(slotName);
            newSlot.transform.SetParent(transform, false);
            newSlot.GetComponent<SaveSlot>().slotName = slotName;
            newSlot.transform.Find("Text").GetComponent<Text>().text = "FILE: " + slotName +
            "\nLAST PLAYED: " + data.dateAndTime;
            newSlot.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                selectManager.ActivateToggleComponentOnly(newSlot);
            });
            saveSlots.Add(newSlot);

            ResortAddNewSlotPrefab();
        }

        public void RemoveExsistingSlot(string slotName)
        {
            Destroy(transform.Find(FormatSlotInspectorName(slotName)).gameObject);
        }

        private void ResortAddNewSlotPrefab()
        {
            GameObject newSlot = Instantiate(addNewSlotPrefab);
            newSlot.transform.SetParent(transform, false);
            newSlot.name = "AddSlot";

            Destroy(addNewSlotPrefab);

            addNewSlotPrefab = newSlot;
        }

        public void AddAllSavedSlots(List<string> files)
        {
            foreach (string file in files)
            {
                AddNewSlot(file);
            }
        }

        #endregion

        #region WindowOpener Methods

        public void OpenCreateFileWindow()
        {
            GameObject window = Instantiate(createNewFileWindow);
            window.transform.SetParent(GameObject.Find("Save").transform, false);
        }

        public void OpenSaveFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "Are sure you want to overwrite Save data for File: "
            + targetSlot.GetComponent<SaveSlot>().slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate {
                SaveFile();
                Destroy(window);});
            
            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
             windowScript.AddMethodToButtonRight(delegate {
                Destroy(window);});

            window.transform.SetParent(GameObject.Find("Save").transform, false);
        }

        public void OpenLoadFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "Are you sure you want to load File: "
            + targetSlot.GetComponent<SaveSlot>().slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate {
                LoadFile();
                Destroy(window);});
            
            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
             windowScript.AddMethodToButtonRight(delegate {
                Destroy(window);});

            window.transform.SetParent(GameObject.Find("Save").transform, false);
        }

        public void OpenDeleteFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "Are you sure you want to delete File: "
            + targetSlot.GetComponent<SaveSlot>().slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate {
                DeleteFile();
                Destroy(window);});
            
            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
             windowScript.AddMethodToButtonRight(delegate {
                Destroy(window);});

            window.transform.SetParent(GameObject.Find("Save").transform, false);
        }

        #endregion

        #region targetSlotOptions

        public void SaveFile()
        {
            if(targetSlot != null)
            {
                player.SavePlayer(targetSlot.GetComponent<SaveSlot>().slotName);
                
                targetSlot.transform.Find("Text").GetComponent<Text>().text = "FILE: " 
                + targetSlot.GetComponent<SaveSlot>().slotName + "\nLAST PLAYED: " + player.dateAndTime;
            }      
        }

        public void LoadFile()
        {
            if(targetSlot != null)
            {
                player.LoadPlayer(targetSlot.GetComponent<SaveSlot>().slotName);
                //Environemtn Datat load
            }  
        }

        public void DeleteFile()
        {
            if(targetSlot != null)
            {
                saveSlots.Remove(targetSlot);
                player.DeletePlayer(targetSlot.GetComponent<SaveSlot>().slotName);
                //Environemtn Datat Delete
                RemoveExsistingSlot(targetSlot.GetComponent<SaveSlot>().slotName);
            }   
        }

        /* Consider another time
        public void RenameFile(string newName)
        {
            if(targetSlot != null)
            {
                DeleteFile();
                targetSlot.
            }
        }
        */

        #endregion

        #region SlotProperties

        private string FormatSlotInspectorName(string slotName)
        {
            return "SaveSlot (" + slotName + ")";
        }

        #endregion
    }
}
