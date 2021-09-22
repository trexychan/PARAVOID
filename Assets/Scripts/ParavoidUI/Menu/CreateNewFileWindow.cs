using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class CreateNewFileWindow : MonoBehaviour
    {
        private InputField inputField;
        private Button buttonCreateNewFile;
        private Button buttonCancel;
        private SlotManager saveContent;

        public void Awake()
        {
            inputField = transform.Find("InputField").gameObject.GetComponent<InputField>();
            buttonCreateNewFile = transform.Find("button_createNewFile").gameObject.GetComponent<Button>();
            buttonCancel = transform.Find("button_cancel").gameObject.GetComponent<Button>();

            saveContent = GameObject.Find("SaveContent").GetComponent<SlotManager>();

            buttonCreateNewFile.onClick.AddListener(delegate {
                saveContent.AddNewSaveFile(inputField.text);
                Destroy(gameObject);});
                
            buttonCancel.onClick.AddListener(delegate { 
                Destroy(gameObject);});
        }

        public void Update()
        {
            if (inputField.text == "" || inputField.text.Contains(" ") || DoesSlotNameExsist(inputField.text))
                buttonCreateNewFile.interactable = false;
            else
                buttonCreateNewFile.interactable = true;


        }

        public bool DoesSlotNameExsist(string slotName)
        {
            foreach (GameObject slot in saveContent.saveSlots)
                if (slot.GetComponent<SaveSlot>().slotName == slotName)
                    return true;

            return false;
        }
    }
}
