using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Objects
    public static PlayerController singleton;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private Rigidbody rb;
   
    //Base Physics
    private Vector3 playerVelocity;

    //Primary Controls
    public PlayerControls playerControls;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isOnMovingPlatform;
    public float playerSpeed = 2.0f;
    public float sensitivity;
    public float jumpForce = 7.5f;
    public float airAcceleration = 1.0f;
    private float jumpTimeWindow = 0.2f;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private Vector3 moveDir;
    private Vector2 moveVect;
    [SerializeField] private Vector2 smoothInputVelocity;
    [SerializeField] private float smoothInputSpeed;
    [SerializeField] private LayerMask whatIsGround;
    
    //Secondary Mechanics    
    [SerializeField] private bool doubleJumpUnlocked = false;
    [SerializeField] private bool hasDoubleJumped;
    [SerializeField] private bool hasJumpedOnce;

    private void Awake()
    {
        singleton = this;
        playerControls = new PlayerControls();
        playerCamera = PlayerCamera.singleton;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public bool IsMoving()
    {
        return moveVect != Vector2.zero;
    }

    public bool IsGrounded() { return isGrounded; }

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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, whatIsGround);
        isOnMovingPlatform = Physics.CheckSphere(groundCheck.position, 0.1f, 1 << 11);
        if (isGrounded)
        {
            hasJumpedOnce = false;
            hasDoubleJumped = false;
            jumpTimeWindow = 0.2f;
        }
        else
        {
            UpdateJumpWindow();
        }

        if (PlayerJumped())
        {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }
        HandleMouseMovement();
        HandleMoveInput();        
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void UpdateJumpWindow()
    {
        jumpTimeWindow -= Time.deltaTime;
    }


    private void HandleMoveInput()
    {
        Vector2 newMoveVect;
        newMoveVect = new Vector2(getPlayerMoveVector().x, getPlayerMoveVector().y);
        
        moveVect = Vector2.SmoothDamp(moveVect, newMoveVect, ref smoothInputVelocity, smoothInputSpeed);


        moveDir = (transform.forward * moveVect.y + transform.right * moveVect.x) * playerSpeed;

        if (jumpBufferCounter > 0f && canJump())
        {
            Jump();
        }
        
    }

    private void HandlePlayerMovement()
    {
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        if (!isGrounded && hasJumpedOnce && canJump())
        {
            Debug.Log("Double Jump");
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            hasJumpedOnce = true;
            hasDoubleJumped = true;  
        } else if (jumpTimeWindow > 0f)
        {
            Debug.Log("Single Jump");
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpTimeWindow = 0f;
            hasJumpedOnce = true;
        }

        jumpBufferCounter = 0f;
        
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

    private bool canJump()
    {
        if (isGrounded || jumpTimeWindow > 0)
        {
            hasJumpedOnce = false;
            return true;
        } else if (doubleJumpUnlocked && !isGrounded && !hasDoubleJumped)
        {
            hasJumpedOnce = true;
            return true;
        }
        return false;
    }

    public void EnableDoubleJump() {
        doubleJumpUnlocked = true;
    }

    
}
