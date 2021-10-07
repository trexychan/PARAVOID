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
        public bool touchRepeated;

        public void Awake()
        {
            touched = false;
            touchRepeated = false;

            uwuText = transform.Find("Text").GetComponent<Text>();

            StartCoroutine(blinking());
        }

        public void Update()
        {
            if (touchRepeated && touched)
            {
                StopAllCoroutines();
                StartCoroutine(uwuing(">w<"));
                touched = false;
            }
            else if (touched)
            {
                StopAllCoroutines();
                StartCoroutine(uwuing("uwu"));
                touched = false;
                touchRepeated = true;
            }
        }

        public void Touch()
        {
            touched = true;
        }

        private IEnumerator uwuing(string uwuFace)
        {
            uwuText.text = uwuFace;

            yield return new WaitForSeconds(1.2f);

            touchRepeated = false;

            StartCoroutine(blinking());
        }

        private IEnumerator blinking()
        {
            while(true)
            {
                uwuText.text = "-w-";
                yield return new WaitForSeconds(0.3f);

                uwuText.text = "OwO";
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
