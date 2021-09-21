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

        public GameObject targetSlot;
        public List<SaveSlot> saveSlots;

        public void Awake()
        {
            if(player != null)
            {
                player.LoadGameFiles();
                AddAllSavedSlots(player.files);
            }
            else
                Debug.LogError("Error: player variable is empty in SlotManger");

            
        }

        #region SlotManger methods

        public void AddNewSaveFile(string slotName)
        {
            player.SavePlayer(slotName);
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
            saveSlots.Add(newSlot.GetComponent<SaveSlot>());

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
            player.SavePlayer(targetSlot.GetComponent<SaveSlot>().slotName);
            targetSlot.transform.Find("Text").GetComponent<Text>().text = "FILE: " 
            + targetSlot.GetComponent<SaveSlot>().slotName + "\nLAST PLAYED: " + player.dateAndTime;
        }

        public void LoadFile()
        {
            player.LoadPlayer(targetSlot.GetComponent<SaveSlot>().slotName);
        }

        public void DeleteFile()
        {
            saveSlots.Remove(targetSlot.GetComponent<SaveSlot>());
            player.DeletePlayer(targetSlot.GetComponent<SaveSlot>().slotName);
            RemoveExsistingSlot(targetSlot.GetComponent<SaveSlot>().slotName);
        }

        public void RenameFile()
        {
            
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
