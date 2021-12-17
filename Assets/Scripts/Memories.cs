using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;
using UnityEngine.UI;

public enum MemoryType
{
    Kalimba = 0,
    HamsterCage = 1,
    GamingConsole = 2
}

public class Memories : MonoBehaviour
{
    public MemoryType memory;
    public Sprite[] memory1Sprites;
    public Sprite[] memory2Sprites;
    public Sprite[] memory3Sprites;
    private TextProducer dialogue;
    private CanvasGroup canvasFade;
    private Image faderImage;
    private Fader fader;
    private AudioManager audioManager;
    private Color fadeColorBox;
    public void Awake()
    {
        dialogue = GameObject.Find("DialogueText").GetComponent<TextProducer>();
        canvasFade = GameObject.Find("Fader").GetComponent<CanvasGroup>();
        faderImage = GameObject.Find("Fader").GetComponent<Image>();
        fader = GameObject.Find("VisualCanvas").GetComponent<Fader>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        fadeColorBox = faderImage.color;
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
        StartCoroutine(ActivateMemory(other.gameObject));
    }

    private IEnumerator ActivateMemory(GameObject player)
    {
        var playerController = player.GetComponent<PlayerController>();
        Destroy(GetComponent<BoxCollider>());
        audioManager.Play("thoomp");
        fader.MemoryFading(1f, 10f);
        playerController.DisableControls();
        yield return new WaitForSeconds(2f);

        fadeColorBox = new Color(255f, 255f, 255f);
        faderImage.color = fadeColorBox;

        switch (memory)
        {
            case MemoryType.Kalimba:
                dialogue.ReplaceText("A small kiosk sold small niche things like this.", Effect.Type, 0.045f);
                faderImage.sprite = memory1Sprites[0];
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("It sounded so pretty...", Effect.Type, 0.045f);
                faderImage.sprite = memory1Sprites[1];
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I wish she continued learning...", Effect.Type, 0.1f);
                yield return new WaitForSeconds(7f);
                dialogue.ReplaceText("Our parents shouldn't have pressured her so much.", Effect.Type, 0.045f);
                faderImage.sprite = memory1Sprites[2];
                yield return new WaitForSeconds(5f);
                dialogue.ReplaceText("...grew up too fast.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(3.5f);
                fadeColorBox = new Color(0f, 0f, 0f);
                faderImage.color = fadeColorBox;
                dialogue.ReplaceText("She", Effect.Type, 0.045f);
                yield return new WaitForSeconds(1.4f);
                dialogue.RunText(" changed too much.", Effect.Type, 0.06f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("She called me a child for acting the way I did", Effect.Type, 0.045f);
                fadeColorBox = new Color(255f, 255f, 255f);
                faderImage.color = fadeColorBox;
                faderImage.sprite = memory1Sprites[3];
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
                faderImage.sprite = memory1Sprites[4];
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("and I", Effect.Type, 0.1f);
                yield return new WaitForSeconds(2f);
                dialogue.ReplaceText(" remained a kid", Effect.Type, 0.12f);
                faderImage.sprite = memory1Sprites[5];
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("Wait...", Effect.Type, 0.2f);
                faderImage.sprite = memory1Sprites[6];
                yield return new WaitForSeconds(5f);
                dialogue.ReplaceText("Who was that?", Effect.Type, 0.2f);
                faderImage.sprite = memory1Sprites[7];
                yield return new WaitForSeconds(6f);
                break;

            case MemoryType.HamsterCage:
                dialogue.ReplaceText("I remember keeping a rat in this.", Effect.Type, 0.045f);
                faderImage.sprite = memory2Sprites[0];
                yield return new WaitForSeconds(3.5f);
                dialogue.ReplaceText("I thought it was an unwell hamster when I found it.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("Someone told me it was a rat.", Effect.Type, 0.045f);
                faderImage.sprite = memory2Sprites[1];
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I said it was a hamster.", Effect.Type, 0.045f);
                faderImage.sprite = memory2Sprites[2];
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I guess they just went along.", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("It disappeared one day.", Effect.Type, 0.06f);
                faderImage.sprite = memory2Sprites[3];
                yield return new WaitForSeconds(3.5f);
                dialogue.RunText(" She went off as well.", Effect.Type, 0.09f);
                fadeColorBox = new Color(0f, 0f, 0f);
                faderImage.color = fadeColorBox;
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("But that was years later.", Effect.Type, 0.09f);
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("Strange enough", Effect.Type, 0.07f);
                yield return new WaitForSeconds(2f);
                dialogue.RunText(", I do remember seeing it recently", Effect.Type, 0.045f);
                yield return new WaitForSeconds(4f);
                dialogue.RunText("--the rat, I mean.", Effect.Type, 0.045f);
                fadeColorBox = new Color(255f, 255f, 255f);
                faderImage.color = fadeColorBox;
                faderImage.sprite = memory2Sprites[4];
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("When was that?", Effect.Type, 0.06f);
                faderImage.sprite = memory2Sprites[5];
                yield return new WaitForSeconds(3f);
                dialogue.ReplaceText("Where?", Effect.Type, 0.3f);
                faderImage.sprite = memory2Sprites[6];
                yield return new WaitForSeconds(6f);
                break;

            case MemoryType.GamingConsole:
                dialogue.ReplaceText("We played so many games on this.", Effect.Type, 0.05f);
                faderImage.sprite = memory3Sprites[0];
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("It was so fun", Effect.Type, 0.05f);
                faderImage.sprite = memory3Sprites[1];
                yield return new WaitForSeconds(2f);
                dialogue.RunText(", back before we fought all the time.", Effect.Type, 0.05f);
                yield return new WaitForSeconds(4f);
                dialogue.ReplaceText("I beat her in Street Fighter", Effect.Type, 0.06f);
                faderImage.sprite = memory3Sprites[2];
                yield return new WaitForSeconds(3f);
                dialogue.RunText(" but June beat me in Mario Kart.", Effect.Type, 0.06f);
                StartCoroutine(MarioKart());
                yield return new WaitForSeconds(6f);
                dialogue.ReplaceText("She was the better driver after all", Effect.Type, 0.07f);
                StartCoroutine(DriveFade());
                yield return new WaitForSeconds(2.6f);
                dialogue.RunText("...", Effect.Type, 0.2f);
                yield return new WaitForSeconds(5f);
                break;
        }

        fadeColorBox = new Color(0f, 0f, 0f);
        faderImage.color = fadeColorBox;
        dialogue.ReplaceText("", Effect.None, 1f);
        fader.MemoryFading(0f, 10f);
        audioManager.Play("thoomp");

        if (GameObject.Find("Player").GetComponent<Player>().Memories >= 3)
        {
            GameObject.Find("DialogueText").GetComponent<TextProducer>().ReplaceTextFor(
                "All Memories Collected", Effect.Type, 0.04f, 8f, true);
        }

        playerController.EnableControls();
        gameObject.SetActive(false);
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

    private IEnumerator MarioKart()
    {
        faderImage.sprite = memory3Sprites[3];

        yield return new WaitForSeconds(2f);

        faderImage.sprite = memory3Sprites[4];

        yield return new WaitForSeconds(2f);

        faderImage.sprite = memory3Sprites[5];
    }

    private IEnumerator DriveFade()
    {
        faderImage.sprite = memory3Sprites[6];

        yield return new WaitForSeconds(2.53f);

        faderImage.sprite = memory3Sprites[7];

        yield return new WaitForSeconds(2.53f);

        faderImage.sprite = memory3Sprites[8];

        yield return new WaitForSeconds(2.53f);
    }
}
