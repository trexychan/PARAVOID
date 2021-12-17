using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ParavoidUI;

public class ButtonBehavior : MonoBehaviour
{
    public Button mainMenuButt;
    public Button exitGameButt;
    public Button backButt;

    public void Awake()
    {
        mainMenuButt = transform.Find("button_returnToMainMenu").gameObject.GetComponent<Button>();
        exitGameButt = transform.Find("button_exitGame").gameObject.GetComponent<Button>();
        backButt = transform.Find("button_back").gameObject.GetComponent<Button>();

        mainMenuButt.onClick.AddListener(delegate
        {
            SceneLoader.LoadScene("TitleMaster");
        });

        exitGameButt.onClick.AddListener(delegate
        {
            Application.Quit();
        });

        backButt.onClick.AddListener(delegate
        {
            transform.parent.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            Time.timeScale = 1;
        });
    }


}
