using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagement;
using ParavoidUI;
/**
* Consider that 
*/
public class Checkpoint : MonoBehaviour
{
    public Player player;
    public SlotManager slotManager;

    public void Awake() 
    {
        player = GameObject.FindObjectsOfType<Player>()[GameObject.FindObjectsOfType<Player>().Length - 1]
            .GetComponent<Player>();
        slotManager = GameObject.Find("VisualCanvas").transform.Find("MenuPanelV2")
            .Find("MenuTabs").Find("Save").Find("SlotManagement").Find("Viewport")
            .Find("SaveContent").gameObject.GetComponent<SlotManager>();
           
        name = "Checkpoint";
    }   
    
    private void OnTriggerEnter(Collider collider) 
    {      
        string name = player.playerFileName;

        if (name == "")
        {
            name = "Save 1";
        }
        char why = name[name.Length - 1];
        
        int slotNum = int.Parse(char.ToString(why));

        player.SavePlayer(name);

        if (slotManager.saveSlots.Count > 0)
        {   
            slotManager.targetSlot = slotManager.saveSlots[slotNum - 1];
            slotManager.SaveFile();
        }

        //Drew's crazy bullshit
        //Technally we don't use this but here
        int test = why - '0';
        print(test);

        Destroy(gameObject);
    }
}