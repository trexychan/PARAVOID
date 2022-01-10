using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    [SerializeField] private float timeMultiplier = 0.5f;
    [SerializeField] private float slowTimeLength = 5.0f;
    [SerializeField] private float slowTimeCooldown = 5.0f;
    public bool slowTimeEnable = false;

    private bool slow = false;
    private bool hasSlowed = false;
    private float startSlowTime = 0f;
    private float startCooldown = 0f;
    private float slowJumpForce, regularJumpForce;

    private PlayerController surf;
    private Rigidbody rb;
    private ConstantForce slowGrav;

    void Awake()
    {
        surf = transform.GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        regularJumpForce = surf.jumpForce;
        // IDK why, but the 1.05 feels more accurate to how high the player actually jumps
        slowJumpForce = regularJumpForce / (1.05f * timeMultiplier);
    }

    void FixedUpdate()
    {   
        if(slowTimeEnable) 
        {
            float diffSlowTime = Time.time - startSlowTime;
            float diffCooldown;
            
            //Here so that the player can use the slow time function at time = 0
            if(!hasSlowed)
            {
                diffCooldown = slowTimeCooldown;
            }
            else
            {
                diffCooldown = Time.time - startCooldown;
            }

            //Right click default key to activate ability
            if(surf.playerControls.Player.TimeAbility.triggered && !slow && diffCooldown >= slowTimeCooldown)
            {
                Debug.Log("Slowing down now!");
                ApplyTimeMult(timeMultiplier);
                startSlowTime = Time.time;

                // Increasing the force of gravity so that the player falls faster (or as close to as fast as they fall in normal time)
                rb.useGravity = false;
                slowGrav = gameObject.AddComponent<ConstantForce>();
                // IDK why mathematically, this 1.75 is there but it feels more accurate
                slowGrav.force = (Physics.gravity * 1.75f) / timeMultiplier;

                // Increasing the jump force to counteract the greater gravity
                surf.jumpForce = slowJumpForce;

                slow = true;
                hasSlowed = true;
            }
            else if (diffSlowTime >= slowTimeLength && slow)
            {
                Debug.Log("Stopping slow time");
                ApplyTimeMult(1/timeMultiplier);

                rb.useGravity = true;
                Destroy(slowGrav);
                surf.jumpForce = regularJumpForce;

                startCooldown = Time.time;
                slow = false;
            }
        }
    }

    private void ApplyTimeMult(float mult)
    {
        Time.timeScale *= mult;
        Time.fixedDeltaTime *= mult;

        // Counteracting the time slow by increasing the parameters of player speed
        surf.playerSpeed /= mult;
        PlayerCamera.singleton.mouseSensitivity /= mult;
    }
}

