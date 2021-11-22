using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockCounter : MonoBehaviour
{
    public enum Type
    {
        None,
        Smooth
    }
    public Image filling;
    public Text timeTrack;
    public bool clockStart;


    // Start is called before the first frame update
    void Start()
    {
        filling = GetComponent<Image>();
        timeTrack = transform.Find("Text").GetComponent<Text>();
        filling.fillAmount = 1;
        filling.enabled = false;

        RunTimer(30f, Type.None);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunTimer(float seconds, Type type)
    {
        filling.enabled = true;
        StartCoroutine(Counter(seconds, type == Type.Smooth ? 0.001f : 1f));
    }

    private IEnumerator Counter(float seconds, float delayTick)
    {
        float timeLeft = seconds;
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(delayTick);

            timeTrack.text = ((int)timeLeft).ToString();
            filling.fillAmount = timeLeft / seconds;

            timeLeft -= delayTick;
        }
        filling.enabled = false;
    }
}
