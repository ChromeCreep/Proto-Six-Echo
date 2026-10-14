using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Basic2DMovement : MonoBehaviour
{
    //Variables important to the player moving.
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    
    //Variables important to the player jumping.
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private BoxCollider2D groundedChecker;
    public bool isGrounded = true;

    //Variables to allow jump buffering.
    [Header("Jump Buffer Settings")]
    [SerializeField] private float jumpBufferTimer;
    private float originalJumpBufferTimer;
    bool jumpBuffered = false;
    bool hasJumped = false;

    [Header("Fast Fall Properties")]
    [SerializeField] private float fastFallSpeed;
    bool fastFallActive = false;
    private float originalGravityScale;
    [SerializeField] private float yVelocity;

    [Header("Coyote Time Settings")]
    [SerializeField] private float coyoteTimer;
    private float originalCoyoteTimer;
    bool coyoteTime = true;

    public Transform startingPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originalJumpBufferTimer = jumpBufferTimer;
        originalCoyoteTimer = coyoteTimer;
        originalGravityScale = rb.gravityScale;
        transform.position = startingPosition.position;
    }

    void Update()
    {
        //Basic Movement Logic
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        
        rb.linearVelocity = new Vector2(horizontalAxis * moveSpeed, rb.linearVelocity.y);
        //Jump Logic
        CheckIfJumpIsBuffered();
        CheckIfCoyoteTime();
        
        yVelocity = rb.linearVelocity.y;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBuffered = true;
        }
        
        if ((coyoteTime && jumpBuffered) || (isGrounded && jumpBuffered))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            jumpBufferTimer = 0;
            coyoteTimer = 0;
            isGrounded = false;
            hasJumped = true;
        }

        if (hasJumped && isGrounded)
        {
            hasJumped = false;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded == false)
            {
                fastFallActive = true;
            }
        }

        // Dash

        FastFall();
    }

    void FastFall()
    {
        if (fastFallActive)
        {
            if (isGrounded == false)
            {
                rb.gravityScale = fastFallSpeed;
            }
        }
    }

    void CheckIfJumpIsBuffered()
    {
        if (jumpBuffered)
        {
            jumpBufferTimer -= Time.deltaTime;
            if (jumpBufferTimer <= 0)
            {
                jumpBuffered = false;
            }
        }
        else if (jumpBuffered == false)
        {
            jumpBufferTimer = originalJumpBufferTimer;
        }
    }
    
    void CheckIfCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimer = originalCoyoteTimer;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (coyoteTimer > 0)
        {
            coyoteTime = true;
        }
        else
        {
            coyoteTime = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.gravityScale = originalGravityScale;
            fastFallActive = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void ResetPosition(InputAction.CallbackContext context)
    {
        transform.position = startingPosition.position;
    }
}
