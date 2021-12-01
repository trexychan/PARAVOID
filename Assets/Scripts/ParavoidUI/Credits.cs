using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    public class Credits : MonoBehaviour
    {
        private CanvasGroup creditFade;
        private TextProducer title;
        private TextProducer textLeft;
        private TextProducer textLeftSub;
        private TextProducer textRight;
        private TextProducer textRightSub;
        private TextProducer textMiddle;
        private TextProducer textMiddleSub;
        private TextProducer textBotRight;
        private TextProducer textBotRightSub;
        private TextProducer textBotLeft;
        private TextProducer textBotLeftSub;
        private TextProducer textPeople;

        private Image frame;


        public void Awake()
        {
            creditFade = GetComponent<CanvasGroup>();
            textLeft = transform.Find("TextLeft").GetComponent<TextProducer>();
            textLeftSub = transform.Find("TextLeftSub").GetComponent<TextProducer>();
            textRight = transform.Find("TextRight").GetComponent<TextProducer>();
            textRightSub = transform.Find("TextRightSub").GetComponent<TextProducer>();
            textMiddle = transform.Find("TextMiddle").GetComponent<TextProducer>();
            textMiddleSub = transform.Find("TextMiddleSub").GetComponent<TextProducer>();
            textBotRight = transform.Find("TextBotRight").GetComponent<TextProducer>();
            textBotRightSub = transform.Find("TextBotRightSub").GetComponent<TextProducer>();
            textBotLeft = transform.Find("TextBotLeft").GetComponent<TextProducer>();
            textBotLeftSub = transform.Find("TextBotLeftSub").GetComponent<TextProducer>();
            textPeople = transform.Find("TextPeople").GetComponent<TextProducer>();
            title = transform.Find("Title").GetComponent<TextProducer>();
            frame = transform.Find("frame").GetComponent<Image>();
        }

        public void SkipCredits()
        {
            StartCoroutine(fadeThisShit(0f, 10f));
        }

        public void RollCredits()
        {
            StartCoroutine(fadeThisShit(1f, 10f));
            StartCoroutine(CreditsMaker());
        }

        private IEnumerator CreditsMaker()
        {
            title.RevertText(Effect.None, 0f);
            textLeft.RevertText(Effect.None, 0f);
            textLeftSub.RevertText(Effect.None, 0f);
            textRight.RevertText(Effect.None, 0f);
            textRightSub.RevertText(Effect.None, 0f);
            textBotLeft.RevertText(Effect.None, 0f);
            textBotLeftSub.RevertText(Effect.None, 0f);
            textBotRight.RevertText(Effect.None, 0f);
            textBotRightSub.RevertText(Effect.None, 0f);
            textMiddle.RevertText(Effect.None, 0f);
            textMiddleSub.RevertText(Effect.None, 0f);

            yield return new WaitForSeconds(2f);

            //Team Leads: Rolling Ethan and Sara
            title.ReplaceText("Team Lead", Effect.Fade, 3f);
            StartCoroutine(FadeImage(frame, 2f));
            textLeft.ReplaceText("Ethan Yu", Effect.Type, 0.1f);
            textLeftSub.ReplaceText("Programming, Environment Art", Effect.Type, 0.06f);
            textRight.ReplaceText("Sara Inani", Effect.Type, 0.1f);
            textRightSub.ReplaceText("Asset Design, UI/UX, Story", Effect.Type, 0.06f);

            yield return new WaitForSeconds(5f);

            //StartCoroutine(FadeRevertImage(frame, 3f));
            title.RevertText(Effect.Fade, 3f);
            textLeft.RevertText(Effect.Fade, 3f);
            textLeftSub.RevertText(Effect.Fade, 3f);
            textRight.RevertText(Effect.Fade, 3f);
            textRightSub.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            /*Sub Team Leads: Alan Tao [Story], Jason [Lvl Design], Ben Lock [Art]
            Drew Busch [Music], Andrew Gao [Programming]
            */
            title.ReplaceText("Sub Team Leads", Effect.Fade, 2f);
            //StartCoroutine(FadeImage(frame, 2f));
            textLeft.ReplaceText("Jason Lei", Effect.Type, 0.1f);
            textLeftSub.ReplaceText("Level Design Lead", Effect.Type, 0.06f);
            textMiddle.ReplaceText("Ben Lock", Effect.Type, 0.1f);
            textMiddleSub.ReplaceText("Art Lead", Effect.Type, 0.06f);
            textRight.ReplaceText("Andrew Gao", Effect.Type, 0.1f);
            textRightSub.ReplaceText("Programming Lead", Effect.Type, 0.06f);
            textBotLeft.ReplaceText("Drew Busch", Effect.Type, 0.1f);
            textBotLeftSub.ReplaceText("Music Lead", Effect.Type, 0.06f);
            textBotRight.ReplaceText("Alan Tao", Effect.Type, 0.1f);
            textBotRightSub.ReplaceText("Story Lead", Effect.Type, 0.06f);

            yield return new WaitForSeconds(7f);

            //StartCoroutine(FadeRevertImage(frame, 2f));
            title.RevertText(Effect.Fade, 3f);
            textLeft.RevertText(Effect.Fade, 3f);
            textLeftSub.RevertText(Effect.Fade, 3f);
            textRight.RevertText(Effect.Fade, 3f);
            textRightSub.RevertText(Effect.Fade, 3f);
            textBotLeft.RevertText(Effect.Fade, 3f);
            textBotLeftSub.RevertText(Effect.Fade, 3f);
            textBotRight.RevertText(Effect.Fade, 3f);
            textBotRightSub.RevertText(Effect.Fade, 3f);
            textMiddle.RevertText(Effect.Fade, 3f);
            textMiddleSub.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            title.ReplaceText("Level Design Team", Effect.Fade, 2f);
            //StartCoroutine(FadeImage(frame, 2f));
            textPeople.ReplaceText("Jason Lei\n"
                                 + "Alex Yang", Effect.Type, 0.06f);

            yield return new WaitForSeconds(5f);

            //StartCoroutine(FadeRevertImage(frame, 2f));
            title.RevertText(Effect.Fade, 3f);
            textPeople.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            title.ReplaceText("Art Team", Effect.Fade, 2f);
            //StartCoroutine(FadeImage(frame, 2f));
            textPeople.ReplaceText("Ben Lock\nRoberto", Effect.Type, 0.06f);

            yield return new WaitForSeconds(5f);

            //StartCoroutine(FadeRevertImage(frame, 2f));
            title.RevertText(Effect.Fade, 3f);
            textPeople.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            title.ReplaceText("Programming Team", Effect.Fade, 2f);
            //StartCoroutine(FadeImage(frame, 2f));
            textPeople.ReplaceText("Andrew Gao\nDrew Bondurant\nTrevor Ferreira\nAvery Greer\nShreshta Yadav\nJade Spooner\nNoura El-Bayrouti", Effect.Type, 0.045f);

            yield return new WaitForSeconds(6f);

            textPeople.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            textPeople.ReplaceText("Reinart Realina\nKevin \"KoolKev246\" Kwan\nMichelle\nDanny George\nRyan Abusaada", Effect.Type, 0.045f);

            yield return new WaitForSeconds(6f);

            //StartCoroutine(FadeRevertImage(frame, 2f));
            title.RevertText(Effect.Fade, 3f);
            textPeople.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            title.ReplaceText("Music Team", Effect.Fade, 2f);
            //StartCoroutine(FadeImage(frame, 2f));
            textPeople.ReplaceText("Drew Busch\nIsaiah McElvain", Effect.Type, 0.045f);

            yield return new WaitForSeconds(6f);

            //StartCoroutine(FadeRevertImage(frame, 2f));
            title.RevertText(Effect.Fade, 3f);
            textPeople.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(1f);

            title.ReplaceText("Story Team", Effect.Fade, 2f);
            //StartCoroutine(FadeImage(frame, 2f));
            textPeople.ReplaceText("Alan Tao\nSaachi\nVibha Raghu", Effect.Type, 0.045f);

            yield return new WaitForSeconds(6f);

            StartCoroutine(FadeRevertImage(frame, 2f));
            title.RevertText(Effect.Fade, 3f);
            textPeople.RevertText(Effect.Fade, 3f);

            yield return new WaitForSeconds(3f);

            SkipCredits();
        }

        private IEnumerator fadeThisShit(float targetAlpha, float speed)
        {
            if (creditFade.alpha < targetAlpha)
            {
                while (creditFade.alpha < targetAlpha)
                {
                    creditFade.alpha += 0.01f;
                    yield return new WaitForSeconds(0.1f / speed);
                }
            }
            else
            {
                while (creditFade.alpha > targetAlpha)
                {
                    creditFade.alpha -= 0.01f;
                    yield return new WaitForSeconds(0.1f / speed);
                }
            }

            if (targetAlpha == 0f)
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
            }
        }

        private IEnumerator FadeImage(Image image, float speed)
        {
            var colorBox = image.color;
            colorBox.a = 0f;
            image.color = colorBox;
            while (image.color.a < 1f)
            {
                colorBox.a += 0.01f;
                image.color = colorBox;
                yield return new WaitForSeconds((0.01f / speed));
            }
        }

        private IEnumerator FadeRevertImage(Image image, float speed)
        {
            var colorBox = image.color;
            while (image.color.a > 0f)
            {
                colorBox.a -= 0.01f;
                image.color = colorBox;
                yield return new WaitForSeconds((0.01f / speed));
            }
        }
    }

}