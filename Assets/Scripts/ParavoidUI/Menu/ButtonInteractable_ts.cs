using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using ParavoidUI;

public class ButtonInteractable_ts : MonoBehaviour
{
    private Button newGameButt;
    private Button continueButt;
    private Button loadButt;
    private Player player;

    public GameObject alertWindow;

    public void Awake() 
    {
        newGameButt = transform.Find("button_newGame").GetComponent<Button>();
        continueButt = transform.Find("button_continue").GetComponent<Button>();
        loadButt = transform.Find("button_load").GetComponent<Button>();

        SetButtonActivity();

        if (GameObject.Find("Player") == null)
        {
            Debug.LogError("Error: Player Does Not Exist in game. I will now destroy game!!");
            return;
        }

        player = GameObject.Find("Player").GetComponent<Player>();

        SetNewSaveButton();
    }

    public void Update()
    {

    }

    //If any files are not empty, then the following buttons will be interactable, otherwise, they will not.
    public void SetButtonActivity()
    {
        foreach (string file in SaveSystem.GetPlayerFiles())
        {
            if (SaveSystem.DeserializePlayerData(file).empty == false)
            {
                Debug.Log("ButtonActivity Ran");
                continueButt.interactable = true;
                loadButt.interactable = true;
                return;
            }
        }
            
        continueButt.interactable = false;
        loadButt.interactable = false;
    }

    public void SetNewSaveButton()
    {
        newGameButt.onClick.RemoveAllListeners();

        if (SaveSystem.GetPlayerFiles().Count <= 0)
        {
            newGameButt.onClick.AddListener(delegate {player.NewPlayer("Save 1");}); 
            return;
        }

        foreach (string file in SaveSystem.GetPlayerFiles())
        {
            if (SaveSystem.DeserializePlayerData(file).empty == true)
            {
                newGameButt.onClick.AddListener(delegate {player.NewPlayer(file);}); 
                return;
            }
        }

        newGameButt.onClick.AddListener(delegate {OpenFilesFullWindow();});

    }

    public void OpenFilesFullWindow()
    {
        GameObject window = Instantiate(alertWindow);
        AlertWindow windowScript = window.GetComponent<AlertWindow>();
        window.transform.SetParent(GameObject.Find("TitlePanel").transform, false);

        windowScript.message.text = "File Slots are Full, please delete existing files to create new ones.";

        windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "Ok";
        Vector3 pos = windowScript.buttonLeft.gameObject.GetComponent<RectTransform>().anchoredPosition;
        pos.x = 0;
        windowScript.buttonLeft.gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
        windowScript.AddMethodToButtonLeft(delegate {
        Destroy(window);});

        Destroy(windowScript.buttonRight.gameObject);
    }


}
