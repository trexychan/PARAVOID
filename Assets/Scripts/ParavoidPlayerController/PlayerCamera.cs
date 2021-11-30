using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera singleton;
    public Transform player;
    private float horizontalAngle;
    public float mouseSensitivity = 0.001f;
    private float verticalAngle;
    private float pitchAngle = 0f;
    private float minPivot = -90f;
    private float maxPivot = 90f;
    
    
    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

    public void CameraRotation(float delta, float mouseX, float mouseY)
    {
        horizontalAngle = mouseX * mouseSensitivity * delta;
        verticalAngle = mouseY * mouseSensitivity * delta;

        pitchAngle -= verticalAngle;
        pitchAngle = Mathf.Clamp(pitchAngle, minPivot, maxPivot);
        
        this.transform.localRotation = Quaternion.Euler(pitchAngle, 0f, 0f);
        player.Rotate(Vector3.up * horizontalAngle);
    }
}
        
    
