using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;
using UnityEngine.UI;

public enum MemoryType
{
    One = 0,
    Two = 1,
    Three = 2
}

public class Memories : MonoBehaviour
{
    public MemoryType memory;
    private TextProducer dialogue;
    private CanvasGroup canvasFade;
    private AudioManager audioManager;
    public void Awake()
    {
        dialogue = GameObject.Find("DialogueText").GetComponent<TextProducer>();
        canvasFade = GameObject.Find("Fader").GetComponent<CanvasGroup>();
        audioManager = GetComponent<AudioManager>();
    }
    public void Start()
    {
        if (GameObject.Find("Player").GetComponent<Player>().memoryCollected[(int)memory] == true)
        {
            transform.position = new Vector3(0f, -100000f, 0f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(0f, -100000f, 0f);
        other.gameObject.GetComponent<Player>().Memories += 1;
        other.gameObject.GetComponent<Player>().memoryCollected[(int)memory] = true;
        Debug.Log(other.gameObject.GetComponent<Player>().Memories);
        StartCoroutine(ActivateMemory());
    }

    private IEnumerator ActivateMemory()
    {
        audioManager.Play("thoomp");
        StartCoroutine(fadeThisShit(1f, 10f));
        yield return new WaitForSeconds(2f);

        switch (memory)
        {
            case MemoryType.One:
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
                break;

            case MemoryType.Two:
                dialogue.ReplaceText("I remember keeping a rat in this.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("I thought it was an unwell hamster when I found it.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3.5f);
                dialogue.ReplaceText("June told me it was a rat.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("I said it was a hamster.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("I guess she just went along.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("It disappeared one day.", Effect.Type, 0.06f);
                yield return new WaitForSeconds(4f);
                dialogue.RunText(" June went off as well.", Effect.Type, 0.09f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("But that was years later.", Effect.Type, 0.09f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("Strange enough", Effect.Type, 0.07f);
                yield return new WaitForSeconds(3f);
                dialogue.RunText(", I do remember seeing it recently", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3f);
                dialogue.RunText("--the rat, I mean.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText("When was that?", Effect.Type, 0.06f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("Where?", Effect.Type, 0.3f);
                yield return new WaitForSeconds(6f);
                break;

            case MemoryType.Three:
                dialogue.ReplaceText("We played so many games on this.", Effect.Type, 0.05f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("It was so fun", Effect.Type, 0.05f);
                yield return new WaitForSeconds(2f);
                dialogue.RunText(", back before we fought all the time.", Effect.Type, 0.05f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I beat her in Street Fighter", Effect.Type, 0.06f);
                yield return new WaitForSeconds(3f);
                dialogue.RunText(" but June beat me in Mario Kart.", Effect.Type, 0.06f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("She was the better driver after all", Effect.Type, 0.07f);
                yield return new WaitForSeconds(2.5f);
                dialogue.RunText("...", Effect.Type, 0.2f);
                yield return new WaitForSeconds(5f);

                break;
        }

        dialogue.ReplaceText("", Effect.None, 1f);
        StartCoroutine(fadeThisShit(0f, 10f));
        audioManager.Play("thoomp");
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
