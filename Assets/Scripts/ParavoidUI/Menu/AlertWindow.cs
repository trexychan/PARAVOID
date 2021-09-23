using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ParavoidUI
{
    public class AlertWindow : MonoBehaviour
    {
        public Text message;
        public Button buttonLeft;
        public Button buttonRight;

        public void Awake()
        {
            message = transform.Find("message").gameObject.GetComponent<Text>();
            buttonLeft = transform.Find("button_leftButton").gameObject.GetComponent<Button>();
            buttonRight = transform.Find("button_rightButton").gameObject.GetComponent<Button>();

            //Default Values
            message.text = "[Empty Message Space]";
            buttonLeft.gameObject.transform.Find("Text").GetComponent<Text>().text = "Left Button";
            buttonRight.gameObject.transform.Find("Text").GetComponent<Text>().text = "Right Button";
        }

        public void AddMethodToButtonLeft(UnityAction call)
        {
            buttonLeft.onClick.AddListener(call);
        }

        public void AddMethodToButtonRight(UnityAction call)
        {
            buttonRight.onClick.AddListener(call);
        }

    }
}
