using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class Fader : MonoBehaviour
    {
        //private Image screen;
        private bool isFaded;

        private Player player;

        public KeyCode ToggleMenuPanel = KeyCode.Escape;
        private CanvasGroup canvasFade;
        private TextProducer dialogueText;

        public void Awake()
        {
            //screen = GetComponent<Image>();
            //UnFadeScreen();
        }

        public void Start()
        {
            dialogueText = GameObject.Find("DialogueText").GetComponent<TextProducer>();
            canvasFade = GameObject.Find("Fader").GetComponent<CanvasGroup>();
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        public void Update()
        {
            UpdateGeneralInput();
        }

        public void UpdateGeneralInput()
        {
            if (Input.GetKeyDown(ToggleMenuPanel) && !player.death)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "MenuPanelV2")
                    {
                        if (child.gameObject.activeInHierarchy)
                        {
                            Debug.Log("Deactive: " + child.gameObject.name);
                            Time.timeScale = 1f;
                            Cursor.lockState = CursorLockMode.Confined;
                            Cursor.visible = false;
                            child.gameObject.SetActive(false);
                            transform.Find("MenuPanelV2").Find("MenuTabs").gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Active: " + child.gameObject.name);
                            Time.timeScale = 0f;
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            child.gameObject.SetActive(true);
                        }

                        break;
                    }
                }

            }
        }

        public void SceneTransitioner()
        {
            StartCoroutine(fadeThisShit(1f, 15f));
            StartCoroutine(niceLoader());
        }

        private IEnumerator niceLoader()
        {
            while (true)
            {
                dialogueText.ReplaceText("Loading...", Effect.Type, 0.2f);
                yield return new WaitForSeconds(5f);
            }
        }

        private IEnumerator fadeThisShit(float targetAlpha, float speed)
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

