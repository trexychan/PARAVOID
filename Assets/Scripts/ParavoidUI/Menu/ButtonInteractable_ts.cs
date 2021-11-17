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

    private int currentEmptyFiles;

    public GameObject alertWindow;

    public void Awake()
    {
        newGameButt = transform.Find("button_newGame").GetComponent<Button>();
        continueButt = transform.Find("button_continue").GetComponent<Button>();
        loadButt = transform.Find("button_load").GetComponent<Button>();
        currentEmptyFiles = -1;

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
        if (FilesChanged())
        {
            SetNewSaveButton();
            SetContinueButton();
            SetButtonActivity();
        }
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
            newGameButt.onClick.AddListener(delegate { player.NewPlayer("Save 1"); });
            return;
        }

        foreach (string file in SaveSystem.GetPlayerFiles())
        {
            if (SaveSystem.DeserializePlayerData(file).empty)
            {
                newGameButt.onClick.AddListener(delegate { OpenStartNewGameWindow(file); });
                return;
            }
        }

        newGameButt.onClick.AddListener(delegate { OpenFilesFullWindow("File Slots are Full, please delete existing files to create new ones."); });

    }

    public void SetContinueButton()
    {
        string latestFile = null;
        List<string> files = SaveSystem.GetPlayerFiles();

        continueButt.onClick.RemoveAllListeners();

        if (files.Count <= 0)
        {
            continueButt.onClick.AddListener(delegate { OpenFilesFullWindow("No Save Files to Load"); });
            continueButt.gameObject.transform.Find("SubText").GetComponent<Text>().text = "";
            return;
        }

        for (int i = 0; i < files.Count; i++)
        {
            if (!SaveSystem.DeserializePlayerData(files[i]).empty)
            {
                if (latestFile == null || SaveSystem.DeserializePlayerData(files[i]).dateAndTime > SaveSystem.DeserializePlayerData(latestFile).dateAndTime)
                {
                    latestFile = files[i];
                }
            }
        }

        //latestFile = null; --- hold this

        continueButt.onClick.AddListener(delegate { OpenContinueGameWindow(latestFile != null ? latestFile : "Lol"); });
        continueButt.gameObject.transform.Find("SubText").GetComponent<Text>().text = latestFile != null ? latestFile : "";

    }

    public void OpenFilesFullWindow(string msg)
    {
        GameObject window = Instantiate(alertWindow);
        AlertWindow windowScript = window.GetComponent<AlertWindow>();
        window.transform.SetParent(GameObject.Find("TitlePanel").transform, false);

        RectTransform rectTransform = window.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(530.5799f, 269.3063f);
        rectTransform.offsetMax = new Vector2(-530.5799f, -429.3063f);

        windowScript.message.text = msg;

        windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "Ok";
        Vector3 pos = windowScript.buttonLeft.gameObject.GetComponent<RectTransform>().anchoredPosition;
        pos.x = 0;
        windowScript.buttonLeft.gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
        windowScript.AddMethodToButtonLeft(delegate
        {
            Destroy(window);
        });

        Destroy(windowScript.buttonRight.gameObject);
    }

    public void OpenStartNewGameWindow(string fileBeingSavedIn)
    {
        GameObject window = Instantiate(alertWindow);
        AlertWindow windowScript = window.GetComponent<AlertWindow>();
        window.transform.SetParent(GameObject.Find("TitlePanel").transform, false);

        RectTransform rectTransform = window.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(530.5799f, 269.3063f);
        rectTransform.offsetMax = new Vector2(-530.5799f, -429.3063f);

        windowScript.message.text = "New Game File will be created in " + fileBeingSavedIn + "\n Do you want to start a new game?";

        windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "Yes";
        windowScript.AddMethodToButtonLeft(delegate
        {
            player.NewPlayer(fileBeingSavedIn);
            Destroy(window);
        });

        windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "No";
        windowScript.AddMethodToButtonRight(delegate
        {
            Destroy(window);
        });
    }

    public void OpenContinueGameWindow(string fileToLoad)
    {
        GameObject window = Instantiate(alertWindow);
        AlertWindow windowScript = window.GetComponent<AlertWindow>();
        window.transform.SetParent(GameObject.Find("TitlePanel").transform, false);

        RectTransform rectTransform = window.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(530.5799f, 269.3063f);
        rectTransform.offsetMax = new Vector2(-530.5799f, -429.3063f);

        windowScript.message.text = "Continue from " + fileToLoad + "?";

        windowScript.buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "Yes";
        windowScript.AddMethodToButtonLeft(delegate
        {
            player.LoadPlayer(fileToLoad);
            Destroy(window);
        });

        windowScript.buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "No";
        windowScript.AddMethodToButtonRight(delegate
        {
            Destroy(window);
        });
    }

    private bool FilesChanged()
    {
        List<string> files = SaveSystem.GetPlayerFiles();
        int filesChanged = 0;

        foreach (string file in files)
        {
            if (SaveSystem.DeserializePlayerData(file).empty)
            {
                filesChanged++;
            }
        }

        bool changed = filesChanged != currentEmptyFiles;

        currentEmptyFiles = filesChanged;

        return changed;
    }


}
