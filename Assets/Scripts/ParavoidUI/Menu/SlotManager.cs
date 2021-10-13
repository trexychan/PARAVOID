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

        //SlotManager Settings
        public int fileLimit = 0;
        public bool overwriteMode = false;

        public void Awake()
        {
            selectManager = gameObject.GetComponent<SelectionManagement>();
            //Auto assigns player if their exisists a player GameObject in scene
            try
            {
                player = GameObject.Find("Player").GetComponent<Player>(); 

                AddAllSavedSlots(SaveSystem.GetPlayerFiles());

                if (overwriteMode) //When Overwrite mode on, potentially incinerates previously stored files
                {
                    if (fileLimit > 0 && SaveSystem.GetPlayerFiles().Count != fileLimit) //Readds all files if limit isn't satisfied (Developer's concern only)
                    {
                        ReloadMissingOverwriteFiles();
                    }
                    else if (fileLimit <= 0)
                    {
                        Debug.LogError("Error: Attempting to utlize overwrite mode requires a valid file limit > 0");
                    }
                   
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.LogError(ex); 
                Debug.LogError("Error: SlotManager's player variable has not been assigned a value \n[No GameObject nammed \"Player\" found to auto assign variable]"); 
            }                              
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

        public void AddNewSaveFile(string slotName, bool empty)
        {
            //Checks if saveslot name already exsists
            foreach (GameObject slot in saveSlots)
                if(slot.GetComponent<SaveSlot>().slotName == slotName)
                    return;

            player.SavePlayer(slotName);
            //Add Environment Data and other game specific stuff
            if (empty)
            {
                player.ErasePlayer(slotName);
            }

            AddNewSlot(slotName);
        }

        public void AddNewSlot(string slotName)
        {
            PlayerData data = SaveSystem.DeserializePlayerData(slotName);

            if (data == null)
            {
                Debug.LogError("Error: Attempts to add notexisistent data: " + slotName);
                return;
            }

            //saveSlot
            GameObject newSlot = Instantiate(saveSlotPrefab);
            newSlot.name = FormatSlotInspectorName(slotName);
            newSlot.transform.SetParent(transform, false);
            newSlot.GetComponent<SaveSlot>().slotName = slotName;
            newSlot.transform.Find("Text").GetComponent<Text>().text = 
            !data.empty ? ("FILE: " + slotName + "\nLAST PLAYED: " + data.dateAndTime)
            : ("EMPTY FILE: " + slotName);
            newSlot.GetComponent<SaveSlot>().isFileEmpty = data.empty ? true : false;
            newSlot.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                selectManager.ActivateToggleComponentOnly(newSlot);
            });
            saveSlots.Add(newSlot);

            ResortAddNewSlotPrefab();
        }

        public void RemoveExsistingSlot(string slotName)
        {
            Destroy(transform.Find(FormatSlotInspectorName(slotName)).gameObject);

            ResortAddNewSlotPrefab();
        }

        private void ResortAddNewSlotPrefab()
        {   
            if (addNewSlotPrefab != null && fileLimit > 0 ? SaveSystem.GetPlayerFiles().Count < fileLimit : true)
            {   
                addNewSlotPrefab.SetActive(true);

                GameObject newSlot = Instantiate(addNewSlotPrefab);
                newSlot.transform.SetParent(transform, false);
                newSlot.name = "AddSlot";

                Destroy(addNewSlotPrefab);

                addNewSlotPrefab = newSlot;
            }
            else if (SaveSystem.GetPlayerFiles().Count >= fileLimit)
            {
                addNewSlotPrefab.SetActive(false);
            }
        }

        public void AddAllSavedSlots(List<string> files)
        {
            if (files != null) 
                foreach (string file in files)
                    AddNewSlot(file);
        }

        #endregion

        #region WindowOpener Methods

        public void OpenCreateFileWindow()
        {
            GameObject window = Instantiate(createNewFileWindow);
            window.transform.SetParent(GameObject.Find("Save").transform, false);
        }

        public void OpenCreateArbitraryFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "Create new File?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate {
                AddNewSaveFile(SaveSystem.GetPlayerFiles().Count + 1 + "");
                Destroy(window);});
            
            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
             windowScript.AddMethodToButtonRight(delegate {
                Destroy(window);});
            
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

        public void OpenEraseFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            
            windowScript.message.text = "Are you sure you want to erase File: "
            + targetSlot.GetComponent<SaveSlot>().slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate {
                EraseFile();
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
            window.transform.SetParent(GameObject.Find("Save").transform, false);

            if (overwriteMode)
            {
                windowScript.message.text = "Can't Delete File while in Overwrite Mode, Make Sure Erase is visible";

                windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "Ok";
                Vector3 pos = windowScript.buttonLeft.gameObject.GetComponent<RectTransform>().anchoredPosition;
                pos.x = 0;
                windowScript.buttonLeft.gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
                windowScript.AddMethodToButtonLeft(delegate {
                Destroy(window);});

                Destroy(windowScript.buttonRight.gameObject);

                return;
            }

            windowScript.message.text = "Are you sure you want to delete File: "
            + targetSlot.GetComponent<SaveSlot>().slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate {
                DeleteFile();
                Destroy(window);});
            
            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
             windowScript.AddMethodToButtonRight(delegate {
                Destroy(window);});
        }

        #endregion

        #region targetSlotOptions

        public void SaveFile()
        {
            if(targetSlot != null)
            {
                player.SavePlayer(targetSlot.GetComponent<SaveSlot>().slotName);

                targetSlot.GetComponent<SaveSlot>().isFileEmpty = false;
                
                targetSlot.transform.Find("Text").GetComponent<Text>().text = "FILE: " 
                + targetSlot.GetComponent<SaveSlot>().slotName + "\nLAST PLAYED: " + player.dateAndTime;
            }      
        }

        public void LoadFile()
        {
            if(targetSlot != null && !targetSlot.GetComponent<SaveSlot>().isFileEmpty 
            && SaveSystem.DoesFileExist(targetSlot.GetComponent<SaveSlot>().slotName))
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

        public void EraseFile()
        {
            if(targetSlot != null)
            {
                player.ErasePlayer(targetSlot.GetComponent<SaveSlot>().slotName);
                //Environemtn Datat Delete

                targetSlot.transform.Find("Text").GetComponent<Text>().text = "EMPTY FILE: " 
                + targetSlot.GetComponent<SaveSlot>().slotName;

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

        #region Developer Restricted Methods

        [ContextMenu("IncinerateAllPlayerFiles")]
        private void IncinerateAllPlayerFiles()
        {
            foreach (GameObject slot in saveSlots)
            {
                string fileName = slot.GetComponent<SaveSlot>().slotName;
                player.DeletePlayer(fileName);
                //Environemtn Datat Delete
                RemoveExsistingSlot(fileName);
            }

            saveSlots = new List<GameObject>();
        }

        private void ReloadMissingOverwriteFiles()
        {              
            for (int i = 1; i <= fileLimit; i++)
                if (!SaveSystem.DoesFileExist("Save " + i))
                    AddNewSaveFile("Save " + i, true);
        }

        #endregion
    }
}
