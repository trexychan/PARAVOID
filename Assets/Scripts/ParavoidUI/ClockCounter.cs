using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ClockType
{
    None,
    Smooth
}

public class ClockCounter : MonoBehaviour
{
    public Image filling;
    public Text timeTrack;
    public float timeLeft;
    public bool clockStart;

    void Awake()
    {
        filling = GetComponent<Image>();
        filling.enabled = false;
        filling.fillAmount = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeTrack = transform.Find("Text").GetComponent<Text>();

        timeTrack.text = "";
        //RunTimer(10f, ClockType.None);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunTimer(float seconds, ClockType type)
    {
        filling.enabled = true;
        StartCoroutine(Counter(seconds, type == ClockType.Smooth ? 0.001f : 1f));
    }

    private IEnumerator Counter(float seconds, float delayTick)
    {
        timeLeft = seconds;
        string fmt = "00";

        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(delayTick);
            timeLeft -= delayTick;

            timeTrack.text = ((int)(timeLeft / 60f)).ToString() + ":" + ((int)(timeLeft % 60f)).ToString(fmt);
            filling.fillAmount = timeLeft / seconds;
        }

        filling.enabled = false;
    }
}
