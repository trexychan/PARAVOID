using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class SaveSlot : MonoBehaviour
    {
        public string slotName;

        public bool isFileEmpty = true;

        public void Update()
        {
            if (GetComponent<Toggle>().isOn)
                transform.parent.gameObject.GetComponent<SlotManager>().targetSlot = gameObject;
        }
    }    
}
