using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] private float moveSpeed = 12f; // top speed
    [SerializeField] private float acceleration = 2f; // acceleration to top speed
    [SerializeField] private float jumpSpeed = 3f;
    private float horizontalMovement; // input for horizontal movement input
    private float horizontalSpeed, verticalSpeed; 

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Platformer "cheating" smoothing
    private float jumpBufferTimer;
    [SerializeField] private float jumpBufferTime = 20;
    private float jumpPressedTimer;
    private bool jumpPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool CanJump() {
        return (IsGrounded() || jumpBufferTimer > 0);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            jumpPressedTimer = 60;
        }

        jumpPressed = Input.GetButton("Jump");
        
    }

    private void FixedUpdate() {
        verticalSpeed = rb.velocity.y;

        if (!IsGrounded()) {
            jumpBufferTimer--;
        } else {
            jumpBufferTimer = jumpBufferTime;
        }

        // Redefine acceleration so we can modify it mid run
        float accel = acceleration;
        
        if (Mathf.Sign(horizontalMovement) != Mathf.Sign(horizontalSpeed)) {
            accel *= 2;
        }



        horizontalSpeed += horizontalMovement * accel;

        if (Mathf.Abs(horizontalMovement) < 0.1f) {
            horizontalSpeed *= 0.595f;
        }

        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -moveSpeed, moveSpeed);

        if (CanJump() && jumpPressedTimer > 0) {
            verticalSpeed = jumpSpeed;
            jumpPressedTimer = 0;
            jumpBufferTimer = 0;
        }

        if (!jumpPressed && verticalSpeed > 0) {
            verticalSpeed *= 0.75f;
        }

        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }
}
