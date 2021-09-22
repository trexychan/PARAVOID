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
        public GameObject createNewFileWindow;

        public GameObject targetSlot;
        public List<GameObject> saveSlots;

        public void Awake()
        {
            selectManager = gameObject.GetComponent<SelectionManagement>();
            if(player != null)
            {
                player.LoadGameFiles();
                AddAllSavedSlots(player.files);
            }
            else
                Debug.LogError("Error: player variable is empty in SlotManger");

            
        }

        public void Update()
        {
            foreach (GameObject slot in saveSlots)
                if(slot.GetComponent<Toggle>().isOn)
                    targetSlot = slot;
        }

        public void OpenCreateFileWindow()
        {
            GameObject window = Instantiate(createNewFileWindow);
            window.transform.SetParent(GameObject.Find("Save").transform, false);
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

        public void RenameFile()
        {
            if(targetSlot != null)
            {

            }
        }

        #endregion

        #region SlotProperties

        private string FormatSlotInspectorName(string slotName)
        {
            return "SaveSlot (" + slotName + ")";
        }

        #endregion
    }
}
