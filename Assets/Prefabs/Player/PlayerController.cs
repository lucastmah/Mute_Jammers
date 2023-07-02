using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
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

    private int availableJumps = 0;

    [SerializeField] private AudioClip[] walkingSounds;
    [SerializeField] private AudioSource walkingSource;

    int footstepTimer;
    [SerializeField] private int footstepTime = 30;

    [SerializeField] private StaplerScript stapler;
    [SerializeField] private AudioSource playerJumpSource;
    [SerializeField] private SpriteRenderer myRenderer;
    private int shootTimer;

    bool isFacingRight = true;

    // Player stats
    [SerializeField] private PlayerStats stats;

    // UPGRADE STATS
    public int ShootTime = 5; // frames that need to pass between each gun fire
    public int MaxJumps = 1;
    [SerializeField] public float moveSpeed = 12f; // top speed
    [SerializeField] public float acceleration = 2f; // acceleration to top speed
    [SerializeField] public float jumpSpeed = 3f;
    
    public bool takesFallDamage = false;

    float startY;
    bool wasGroundedLastFrame = false;

    // Visual stretch
    float xStretch = 1;
    float yStretch = 1;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool CanJump() {
        return ((IsGrounded() || jumpBufferTimer > 0) && availableJumps > 0);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
        // Get inputs
        jumpPressed = Input.GetButton("Jump");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        
        // Buffer our jump inputs - if we press jump right before landing we'll still jump
        if (Input.GetButtonDown("Jump")) {
            jumpPressedTimer = 20;
        }


        // Fire the staple
        if (Input.GetButtonDown("Fire1") && shootTimer < 0) {
            int attackAngle = (isFacingRight) ? 1 : -1;
            stapler.FireStaple(attackAngle);
            shootTimer = ShootTime;
        }
    }

    private void PlayFootSound() {
        walkingSource.clip = walkingSounds[UnityEngine.Random.Range(0, walkingSounds.Length)];
        walkingSource.Play();
    }

    private void FixedUpdate() {
        // Visually set the direction the player and stapler are facing
        if (Mathf.Abs(horizontalMovement) > 0) {
            isFacingRight = (Mathf.Sign(horizontalMovement) > 0);
            myRenderer.flipX = !isFacingRight;
            stapler.VisualUpdate(isFacingRight);
        }

        // Store the vertical speed from the rigidbody
        verticalSpeed = rb.velocity.y;

        // Update timers
        jumpPressedTimer--;
        shootTimer--;
        footstepTimer-= (int)Mathf.Abs(horizontalMovement * moveSpeed);

        // Play footstep SFX
        if (footstepTimer < 0 && IsGrounded()) {
            footstepTimer = footstepTime;
            PlayFootSound();
        }

        // Jump buffering
        if (!IsGrounded()) {
            jumpBufferTimer--;
        } else {
            if (jumpBufferTimer != jumpBufferTime) {
                // We're landing, this will fire once (ish?)
                xStretch = 2.4f;
                yStretch = .5f;

                PlayFootSound();
            }
            
            jumpBufferTimer = jumpBufferTime;
            availableJumps = MaxJumps;
        }

        // Fall damage start
        if (jumpBufferTimer == (jumpBufferTime - 1)) {
            startY = transform.transform.position.y;
        }

        // Redefine acceleration so we can modify it mid run
        float accel = acceleration;
        
        // Reduce our acceleration if we're in the air
        if (Mathf.Sign(horizontalMovement) != Mathf.Sign(horizontalSpeed)) {
            if (IsGrounded()) {
                accel *= 0.95f;
            } else {
                accel *= .75f;
            }
        }


        // Calculate the horizontal movement speed
        horizontalSpeed += horizontalMovement * accel;

        if (Mathf.Abs(horizontalMovement) < 0.1f) {
            horizontalSpeed *= 0.595f;
        }

        // Clamp the horizontal speed
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -moveSpeed, moveSpeed);

        // Start jump
        if (CanJump() && jumpPressedTimer > 0) {
            verticalSpeed = jumpSpeed;
            availableJumps--;
            jumpPressedTimer = 0;
            jumpBufferTimer = 0;

            playerJumpSource.Play();
            xStretch = .5f;
            yStretch = 2f;
        }

        // Variable jump height
        if (!jumpPressed && verticalSpeed > 0) {
            verticalSpeed *= 0.875f;
        }


        // Final velocity set
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);

        // Player visual squash and stretch
        xStretch = Mathf.Lerp(xStretch, 1, 0.2f);
        yStretch = Mathf.Lerp(yStretch, 1, 0.2f);

        if (Mathf.Abs(xStretch - 1) < .05f) {
            xStretch = 1;
        }

        if (Mathf.Abs(yStretch - 1) < .05f) {
            yStretch = 1;
        }

        // Apply the stretch
        myRenderer.transform.transform.localScale = new Vector3(xStretch, yStretch, 1);
        
        // Don't stretch the stapler, apply an inverse scale on it
        stapler.transform.transform.localScale = new Vector3(1 / xStretch, 1 / yStretch, 1);


        // Fall damage stuff
        if (wasGroundedLastFrame && !IsGrounded()) {
            startY = transform.position.y;
            //Debug.Log(startY);
        }

        if (!wasGroundedLastFrame && IsGrounded()) {
            //Debug.Log(startY - transform.position.y);
            if (startY - transform.position.y > 2) {
                DoFallDamage();
            }
        }

        wasGroundedLastFrame = IsGrounded();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(stats.health);
        if (collision.gameObject.CompareTag("Enemy Projectile"))
        {
            stats.DamageTaken(collision.gameObject.GetComponent<ProjectileBehavior>().ProjectileClass.damage);
            
        }
    }

    private void DoFallDamage() {
        
    }
}
