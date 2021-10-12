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

        public KeyCode ToggleMenuPanel = KeyCode.Escape;

        public void Awake()
        {
            //screen = GetComponent<Image>();
            UnFadeScreen();
        }

        public void Update()
        {
            UpdateGeneralInput();
        }

        public void UpdateGeneralInput()
        {
            if (Input.GetKeyDown(ToggleMenuPanel))
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "MenuPanelV2")
                    {
                        if (child.gameObject.activeInHierarchy)
                        {
                            Debug.Log("Deactive: " + child.gameObject.name);
                            child.gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Active: " + child.gameObject.name);
                            child.gameObject.SetActive(true);
                        }

                        break;
                    }
                }
               
            }
        }

        public void FadeScreenTo(float fade)
        {
            //screen.CrossFadeAlpha(fade, 2.0f, true);
        }

        public void FadeScreenTo(float fade, float duration)
        {
            //screen.CrossFadeAlpha(fade, duration, true);
        }

        public void UnFadeScreen()
        {
            FadeScreenTo(0);
        }

        public void ToggleFade()
        {
            if(!isFaded)
            {
                isFaded = false; 
                UnFadeScreen();
            } 
            else
                FadeScreenTo(0.4f);
        }
    } 
}

