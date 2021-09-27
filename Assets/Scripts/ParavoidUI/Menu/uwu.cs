using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class uwu : MonoBehaviour
    {
        public Text uwuText;
        public bool touched;

        public void Awake()
        {
            touched = false;

            uwuText = transform.Find("Text").GetComponent<Text>();

            StartCoroutine(uwuing());
        }

        public void Update()
        {
            if (touched)
            {
                StopCoroutine(uwuing());
                uwuText.text = "uwu";
                StartCoroutine(uwuing());
                touched = false;
            }
        }

        public void Touch()
        {
            touched = true;
        }

        private IEnumerator uwuing()
        {
            while(true)
            {
                yield return new WaitForSeconds(3f);

                uwuText.text = "-w-";
                yield return new WaitForSeconds(0.3f);

                uwuText.text = "OwO";
            }
        }
    }
}
