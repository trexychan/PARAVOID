using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerCamera playerCamera;
    private CharacterController char_controller;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    public Transform groundCheck;
    private LayerMask layerMask = 1 << 9;
    private bool isGrounded;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 2.0f;
    public float gravityValue = -9.18f;
    public float jumpTimeWindow = 0.1f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        char_controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        playerCamera = PlayerCamera.singleton;
    }

    void OnEnable()
    {
        playerControls.Enable();
    }    

    void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 getPlayerMoveVector()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 getMouseDeltaVector()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, layerMask);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            jumpTimeWindow = 0.1f;
        }

        HandleMouseMovement();
        HandlePlayerMovement();
        UpdateJumpWindow();
        
    }

    private void UpdateJumpWindow()
    {
        jumpTimeWindow -= Time.deltaTime;
    }


    private void HandlePlayerMovement()
    {
        Vector2 moveDir = new Vector3(getPlayerMoveVector().x, getPlayerMoveVector().y);
        Vector3 move = moveDir.x * transform.right + transform.forward * moveDir.y;

        if (PlayerJumped() && jumpTimeWindow > 0)
        {
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);   
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        char_controller.Move(move * Time.deltaTime * playerSpeed + playerVelocity * Time.deltaTime);
        
    }

    private void HandleMouseMovement()
    {
        float delta = Time.fixedDeltaTime;

        if (playerCamera)
        {
            if (Time.timeScale != 0f)
            {
                playerCamera.CameraRotation(delta, getMouseDeltaVector().x, getMouseDeltaVector().y);
            }
        } else
        {
            playerCamera = PlayerCamera.singleton;
            if (!playerCamera)
            {
                Debug.LogError("Could not find player camera");
            }
        }
    }

    
}
