using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public float timeMultiplier = 0.5f;
    public bool enabled = false;

    private bool slow = false;

    private float regTime;
    private float regFixedTime;

    // Start is called before the first frame update
    void Start()
    {
        regTime = Time.timeScale;
        regFixedTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {   if(enabled) {
            //Designed so that the time scale stuff doesn't get called literally every frame
            //Probably need to change the input but IDK how that works
            if(Input.GetMouseButton(2) && !slow){
                Time.timeScale = regTime * timeMultiplier;
                Time.fixedDeltaTime = regFixedTime * timeMultiplier;
                slow = true;
            }
            else if (!Input.GetMouseButton(2) && slow) {

                Time.timeScale = regTime;
                Time.fixedDeltaTime = regFixedTime;
                slow = false;
            }
        }
    }
}
