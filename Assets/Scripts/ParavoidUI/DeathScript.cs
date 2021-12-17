using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

namespace ParavoidUI
{
    public class DeathScript : MonoBehaviour
    {
        public TextProducer deathMessage;
        public Button returnToMenuButt;
        public Button returnToLastSaveButt;
        public Button loadSaveButt;

        private CanvasGroup canvasFade;
        private Player player;

        public void Awake()
        {
            canvasFade = GetComponent<CanvasGroup>();
            deathMessage = transform.Find("DeathMessage").GetComponent<TextProducer>();
            returnToMenuButt = transform.Find("Buttons").Find("button_returnToMainMenu").GetComponent<Button>();
            returnToLastSaveButt = transform.Find("Buttons").Find("button_returnToLastSave").GetComponent<Button>();
            loadSaveButt = transform.Find("Buttons").Find("button_loadSave").GetComponent<Button>();

            canvasFade.alpha = 0f;

            returnToMenuButt.onClick.AddListener(delegate
            {
                SceneLoader.LoadScene("TitleMaster");
            });
        }

        public void Start()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        public void Update()
        {

        }

        public void SetLastSaveButt()
        {
            returnToLastSaveButt.onClick.RemoveAllListeners();

            //-----------------Add Current latest save--------------------//
            string latestFile = null;
            List<string> files = SaveSystem.GetPlayerFiles();

            foreach (string name in files)
            {
                if (!SaveSystem.DeserializePlayerData(name).empty)
                {
                    if (latestFile == null || SaveSystem.DeserializePlayerData(name).dateAndTime > SaveSystem.DeserializePlayerData(latestFile).dateAndTime)
                    {
                        latestFile = name;
                    }
                }
            }

            returnToLastSaveButt.onClick.AddListener(delegate
            {
                player.LoadPlayer(latestFile);
            });
        }

        public void ActivateDeathPanel(string message)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(FadeTo(1f, 10f));
            deathMessage.RunText(message, Effect.Type, 0.035f);
            SetLastSaveButt();
        }

        private IEnumerator FadeTo(float targetAlpha, float speed)
        {
            if (canvasFade.alpha < targetAlpha)
            {
                while (canvasFade.alpha < targetAlpha)
                {
                    canvasFade.alpha += 0.01f;
                    yield return new WaitForSeconds(0.1f / speed);
                }
            }
            else
            {
                while (canvasFade.alpha > targetAlpha)
                {
                    canvasFade.alpha -= 0.01f;
                    yield return new WaitForSeconds(0.1f / speed);
                }
            }
        }
    }
}
