using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DataManagement;

namespace ParavoidUI
{
    public class SaveSlot : MonoBehaviour
    {
        public string slotName;
        public GameObject currentView;
        public GameObject alertWindow;
        public bool isFileEmpty = true;
        public bool saving;
        public bool oldSystem;
        private Player player;
        private Text emptyText;
        private Text fileText;
        private Text descriptionText;
        private SlotManager slotManager;

        private GameObject saveSlotButt;
        private GameObject loadSlotButt;
        private GameObject eraseSlotButt;

        public void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            emptyText = transform.Find("EmptyText").GetComponent<Text>();
            fileText = transform.Find("FileText").GetComponent<Text>();
            descriptionText = transform.Find("DescriptionText").GetComponent<Text>();
            currentView = transform.Find("CurrentView").gameObject;

            if (!oldSystem)
            {
                saveSlotButt = transform.Find("Buttons").Find("button_saveSlot").gameObject;
                loadSlotButt = transform.Find("Buttons").Find("button_loadSlot").gameObject;
                eraseSlotButt = transform.Find("Buttons").Find("button_eraseSlot").gameObject;

                saveSlotButt.SetActive(false);
                loadSlotButt.SetActive(false);
                eraseSlotButt.SetActive(false);
            }
        }

        public void Start()
        {
            slotManager = transform.parent.GetComponent<SlotManager>();
        }

        public void Update()
        {
            if (oldSystem && GetComponent<Toggle>().isOn)
            {
                slotManager.targetSlot = gameObject;
                return;
            }

            if (!oldSystem && GetComponent<Toggle>().isOn)
            {
                slotManager.targetSlot = gameObject;
                eraseSlotButt.SetActive(true);

                if (saving)
                {
                    saveSlotButt.SetActive(true);
                    loadSlotButt.SetActive(false);
                    eraseSlotButt.SetActive(false);
                }
                else if (!isFileEmpty)
                {
                    saveSlotButt.SetActive(false);
                    loadSlotButt.SetActive(true);
                    eraseSlotButt.SetActive(true);
                }
                else
                {
                    saveSlotButt.SetActive(false);
                    loadSlotButt.SetActive(false);
                    eraseSlotButt.SetActive(false);
                    GetComponent<Toggle>().isOn = false;
                }
            }
            else if (!oldSystem)
            {
                saveSlotButt.SetActive(false);
                loadSlotButt.SetActive(false);
                eraseSlotButt.SetActive(false);
            }
        }

        public void SaveSlotFile()
        {
            player.SavePlayer(this.slotName);
            this.isFileEmpty = false;
            currentView.SetActive(true);
            emptyText.text = "";
            fileText.text = this.slotName + "\n";
            descriptionText.text = "\nLAST PLAYED: " + player.dateAndTime.ToString();
        }

        public void LoadSlotFile()
        {
            if (!this.isFileEmpty && SaveSystem.DoesFileExist(this.slotName))
            {
                player.LoadPlayer(this.slotName);
                //Environemtn Datat load
            }
        }

        public void EraseSlotFile()
        {
            player.ErasePlayer(this.slotName);
            //Environemtn Datat Delete

            currentView.SetActive(false);
            emptyText.text = "Empty " + this.slotName;
            fileText.text = "";
            descriptionText.text = "";
            this.isFileEmpty = true;
        }

        public void OpenSaveFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "SAVE current progress to " + slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate
            {
                SaveSlotFile();
                Destroy(window);
            });

            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
            windowScript.AddMethodToButtonRight(delegate
            {
                Destroy(window);
            });

            window.transform.SetParent(transform.parent.parent.parent.parent, false);
        }

        public void OpenLoadFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "Do you want to LOAD " + slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate
            {
                LoadSlotFile();
                Destroy(window);
            });

            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
            windowScript.AddMethodToButtonRight(delegate
            {
                Destroy(window);
            });

            window.transform.SetParent(transform.parent.parent.parent.parent, false);
        }

        public void OpenEraseFileWindow()
        {
            GameObject window = Instantiate(alertWindow);
            AlertWindow windowScript = window.GetComponent<AlertWindow>();
            windowScript.message.text = "Are you sure you want to ERASE " + slotName + "?";

            windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "YES";
            windowScript.AddMethodToButtonLeft(delegate
            {
                EraseSlotFile();
                Destroy(window);
            });

            windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "NO";
            windowScript.AddMethodToButtonRight(delegate
            {
                Destroy(window);
            });

            window.transform.SetParent(transform.parent.parent.parent.parent, false);
        }

        public static void FormatSlot()
        {

        }
    }
}
