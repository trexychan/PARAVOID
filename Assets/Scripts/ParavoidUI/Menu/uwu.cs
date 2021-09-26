using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class uwu : MonoBehaviour
    {
        public Text uwuText;

        public void Awake()
        {
            uwuText = transform.Find("Text").GetComponent<Text>();

            StartCoroutine(uwuing());
        }

        private IEnumerator uwuing()
        {
            while(true)
            {
                uwuText.text = "OwO";
                yield return new WaitForSeconds(4f);

                uwuText.text = ">w<";
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
