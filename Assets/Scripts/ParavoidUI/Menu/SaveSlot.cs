using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

namespace ParavoidUI
{
    public class SaveSlot : MonoBehaviour
    {
        public string slotName;
        public bool isFileEmpty = true;
        public bool saving;
        public bool oldSystem;
        private Player player;
        private Text slotText;
        private SlotManager slotManager;

        private GameObject saveSlotButt;
        private GameObject loadSlotButt;
        private GameObject eraseSlotButt;

        public void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            slotText = transform.Find("Text").GetComponent<Text>();

            if (!oldSystem)
            {
                saveSlotButt = transform.Find("Buttons").Find("button_saveSlot").gameObject;
                loadSlotButt = transform.Find("Buttons").Find("button_loadSlot").gameObject;
                eraseSlotButt = transform.Find("Buttons").Find("button_eraseSlot").gameObject;
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
                }
                else
                {
                    saveSlotButt.SetActive(false);
                    loadSlotButt.SetActive(true);
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
            slotText.text = "FILE: " + this.slotName + "\nLAST PLAYED: " + player.dateAndTime.ToString();
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

            slotText.text = "EMPTY FILE: " + this.slotName;
        }

        public static void FormatSlot()
        {

        }
    }
}
