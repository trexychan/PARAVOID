using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParavoidUI
{
    /**
    * Make sure to attach this unity script onto a gameobject anywhere on the unity scene.
    * Works properly only for the current set name of the text box : "DialogueText"
    */
    [DisallowMultipleComponent]
    public class DialogueMaker : MonoBehaviour
    {
        private GameObject dialogueText;
        private TextProducer dialogueProducer;
        public Effect effect;
        public float effectAmount;

        public string[] dialogue;
        public float[] dialogueLength;
        public float[] timeBeforeDialogue;

        public void Awake()
        {
            dialogueText = GameObject.Find("DialogueText");
            dialogueProducer = dialogueText.GetComponent<TextProducer>();

            if (dialogue.Length != dialogueLength.Length || dialogue.Length != timeBeforeDialogue.Length)
            {
                Debug.LogError("Please Make sure that the number of dialogue match their length and there are one less time between each dialogue");
            }
        }

        public void Start()
        {
            StartCoroutine(ReadDialogue());
        }

        private IEnumerator ReadDialogue()
        {
            for (int i = 0; i < dialogue.Length; i++)
            {
                yield return new WaitForSeconds(timeBeforeDialogue[i]);
                dialogueProducer.RunTextFor(dialogue[i], effect, effectAmount, dialogueLength[i], false);
            }
        }
    }
}
