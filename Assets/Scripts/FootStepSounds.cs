using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private PlayerController playerController;

    private bool isWalking = false;

    private void Update()
    {
        if (!isWalking && playerController.IsMoving() && playerController.IsGrounded())
        {
            audioSource.Play();
            isWalking = true;
        }
        else if (isWalking && (!playerController.IsMoving() || !playerController.IsGrounded()))
        {
            audioSource.Stop();
            isWalking = false;
        }
    }
}
