using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memories : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        other.gameObject.GetComponent<Player>().Memories += 1;
        Debug.Log(other.gameObject.GetComponent<Player>().Memories);
        StartCoroutine(ActivateMemory());
    }

    private IEnumerator ActivateMemory()
    {
        audioManager.Play("thoomp");
        switch (memory)
        {
            case MemoryType.One:
                StartCoroutine(fadeThisShit(1f, 10f));
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("A small kiosk sold small niche things like this.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("It sounded so pretty when June played it for me.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I wish she continued learning.", Effect.Type, 0.1f);
                yield return new WaitForSeconds(7f);
                dialogue.ReplaceText("Our parents shouldn't have pressured her so much.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(5f);
                dialogue.ReplaceText("June grew up too fast.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3.5f);
                dialogue.ReplaceText("She", Effect.Type, 0.045f);
                yield return new WaitForSeconds(1.4f);
                dialogue.RunText(" changed too much.", Effect.Type, 0.06f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("She called me a child for acting the way I did", Effect.Type, 0.045f);
                yield return new WaitForSeconds(2.5f);
                dialogue.RunText("...", Effect.Type, 0.5f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("How is that fair?", Effect.Type, 0.05f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("I guess that's why we fought a lot.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3f);
                dialogue.RunText(" Because she was so...", Effect.Type, 0.06f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("grown up...", Effect.Type, 0.1f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("and I remained a kid.", Effect.Type, 0.1f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I miss her.", Effect.Type, 0.15f);
                yield return new WaitForSeconds(6f);
                dialogue.ReplaceText("", Effect.None, 1f);
                StartCoroutine(fadeThisShit(0f, 10f));
                break;

            case MemoryType.Two:
                StartCoroutine(fadeThisShit(1f, 10f));
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("I remember keeping a rat in this.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I thought it was a hamster when I found it, near our old place", Effect.Type, 0.045f);
                yield return new WaitForSeconds(2.8f);
                dialogue.RunText(", unwell.", Effect.Type, 0.07f);
                yield return new WaitForSeconds(1.5f);
                dialogue.ReplaceText("June helped...", Effect.Type, 0.045f);
                yield return new WaitForSeconds(1f);
                dialogue.RunText(" she told me it's a rat", Effect.Type, 0.045f);
                yield return new WaitForSeconds(1.5f);
                dialogue.RunText(", I said it's a hamster", Effect.Type, 0.045f);
                yield return new WaitForSeconds(1.5f);
                dialogue.RunText(", I guess she just went along.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(1.5f);
                dialogue.ReplaceText("It disappeared one day. June went off as well, but that was years later.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(5f);
                dialogue.ReplaceText("Strange enough, I do remember seeing it recently--the rat, I mean.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("When was that?", Effect.Type, 0.045f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("Where?", Effect.Type, 0.08f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("", Effect.None, 1f);
                StartCoroutine(fadeThisShit(0f, 10f));
                break;

            case MemoryType.Three:
                StartCoroutine(fadeThisShit(1f, 10f));
                dialogue.ReplaceTextFor(">Wo", Effect.Type, 0.06f, 10f, true);
                yield return new WaitForSeconds(10f);
                dialogue.ReplaceText("", Effect.None, 1f);
                StartCoroutine(fadeThisShit(0f, 10f));
                break;
        }
        audioManager.Play("thoomp");

        yield return new WaitForSeconds(1f);

        if (GameObject.Find("Player").GetComponent<Player>().Memories >= 3)
        {
            dialogue.ReplaceTextFor("All Memories Collected", Effect.Type, 0.04f, 8f, true);
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
