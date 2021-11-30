using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fragsurf.Movement
{
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

        private PlayerController surf;
        private MovementConfig moveConfig;
        private MoveData _moveData;

        void Awake()
        {
            surf = transform.GetComponent<PlayerController>();
            // SurfCharacter surf = transform.GetComponent<SurfCharacter>();
            // moveConfig = surf.moveConfig;
            // _moveData = surf.MoveData;
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
                    slow = true;
                    hasSlowed = true;
                }
                else if (diffSlowTime >= slowTimeLength && slow)
                {
                    ApplyTimeMult(1/timeMultiplier);
                    startCooldown = Time.time;
                    slow = false;
                }
            }
        }

        private void ApplyTimeMult(float mult)
        {
            Time.timeScale *= mult;
            Time.fixedDeltaTime *= mult;


            //IDK entirely why, but this set of movement stuff seems to work in making the player feel like they're not slowing down

            // moveConfig.Friction /= mult;
            // moveConfig.AirAccel /= mult;
            // moveConfig.Gravity /= mult;
            // moveConfig.JumpHeight /= mult;
            // moveConfig.JumpPower /= mult;
            // moveConfig.Accel /= mult;

            // _moveData.WalkFactor /= mult;
            // _moveData.GravityFactor /= mult;
            // _moveData.Velocity /= mult;
        }
    }
}

