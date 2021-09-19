using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManagement : MonoBehaviour
{
    /*  Notes For this Script:
    *   
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
            child.gameObject.GetComponent<Toggle>().SetIsOnWithoutNotify(obj == child.gameObject ? true : false);
    }

    private void DeactivateGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }

}
