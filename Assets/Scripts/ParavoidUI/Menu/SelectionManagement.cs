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

        public GameObject[] considerdObjects;

        public void Awake()
        {
            if (initialToggles.Length > 0)
                foreach (Toggle toggle in initialToggles)
                    toggle.isOn = true;
        }

        public void ActivateGameObjectOnly(GameObject obj)
        {
            foreach (Transform child in transform)
                if (IsObjectConsidered(child.gameObject))
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

        public void DeactiveAllGameObjects()
        {
            foreach (Transform child in transform)
                if (IsObjectConsidered(child.gameObject))
                    child.gameObject.SetActive(false);
        }

        public void DeactiveAllToggleCompoenents()
        {
            foreach (Transform child in transform)
            {
                try
                {
                    child.gameObject.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
                }
                catch (System.Exception)
                {
                    //To just overthrow GameObjects that don't have toggle componenet
                }
            }
        }

        private bool IsObjectConsidered(GameObject obj)
        {
            if (considerdObjects != null && considerdObjects.Length > 0)
            {
                foreach (GameObject considerdObject in considerdObjects)
                    if (considerdObject == obj)
                        return true;

                return false;
            }

            return true;
        }

        private void DeactivateGameObject(GameObject obj)
        {
            obj.SetActive(false);
        }

    }
}


