using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class SelectionManagement : MonoBehaviour
    {
        /*  Notes For this Script:
        *   Intended to automate activation of a specfied gameobject UI componenet
        *   and deactive the rest under the same parent gameobject 
        *   (which this script must be attached to do so)
        */

        public Toggle[] initialToggles;

        public void Awake()
        {
            if(initialToggles.Length > 0)
                foreach (Toggle toggle in initialToggles)
                    toggle.isOn = true;
        }
    
        public void ActivateGameObjectOnly(GameObject obj)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(obj == child.gameObject ? true : false);
        }

        public void ActivateToggleComponentOnly(GameObject obj)
        {
            foreach (Transform child in transform)
            {
                try
                {
                    child.gameObject.GetComponent<Toggle>().SetIsOnWithoutNotify(obj == child.gameObject ? true : false); 
                }
                catch (System.Exception)
                {
                    //To just overthrow GameObjects that don't have toggle componenet
                }
            }
                
        }

        private void DeactivateGameObject(GameObject obj)
        {
            obj.SetActive(false);
        }   

    }
}


