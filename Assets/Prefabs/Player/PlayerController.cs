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
    [SerializeField] private GameObject walkingFX;

    int footstepTimer;
    [SerializeField] private int footstepTime = 30;

    [SerializeField] private StaplerScript stapler;
    [SerializeField] private AudioSource playerJumpSource;
    [SerializeField] private AudioSource playerHurtSource;
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
    public int recoveryTime = 240;


    // Fall damage checking
    float startY;
    bool wasGroundedLastFrame = false;
    float fallDamageDistance = 2.0f; // distance you take fall damage after dropping. 5.0f is approx one scene height
    // Visual stretch
    float xStretch = 1;
    float yStretch = 1;

    int recoveryTimer = 0; // when not zero, we can't move
    bool isInHurtState;


    // PLAYER ANIMATION
    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        stapler.VisualUpdate(isFacingRight);
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        // set up stuff
        PowerupsList p = PowerupsList.GetInstance();
        takesFallDamage = !p.hasNoFallDmg;
        if (p.hasDoubleJump) {
            MaxJumps+=1;
        }
        if (!p.hasBonusMvspd) {
            moveSpeed -= 1.5f;
        }
        if (!p.hasAcceleration) {
            acceleration *= 0.75f;
        }
        if (!p.hasBonusJumpHeight) {
            jumpSpeed -= 2;
        }
    }

    private bool CanJump() {
        return (((IsGrounded() || jumpBufferTimer > 0) && availableJumps == 1) || (availableJumps >= 1));
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        isInHurtState = recoveryTimer > (recoveryTime * 0.8f);
        if (isInHurtState) {
            jumpPressed = false;
            horizontalMovement = 0;
            return;
        }

        // Get inputs
        jumpPressed = Input.GetButton("Jump");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        

        // Buffer our jump inputs - if we press jump right before landing we'll still jump
        if (Input.GetButtonDown("Jump")) {
            jumpPressedTimer = 20;
            GameObject wFX = Instantiate(walkingFX, new Vector3(transform.position.x, transform.position.y - 1), new Quaternion());
            Destroy(wFX, 1);
        }


        // Fire the staple
        if (Input.GetButtonDown("Fire1") && shootTimer < 0) {
            float attackAngle = (isFacingRight) ? 0 : 180;
            stapler.FireStaple(attackAngle, stats.attack);
            if (PowerupsList.GetInstance().hasDoubleProjectiles)
            {
                stapler.FireStaple(attackAngle, stats.attack);
            }
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
            // PLAYER MOVING
            animator.SetBool("isWalking", true);

            isFacingRight = (Mathf.Sign(horizontalMovement) > 0);

            // Flip the player's sprite
            myRenderer.flipX = !isFacingRight;
            stapler.VisualUpdate(isFacingRight);
        } else {
            animator.SetBool("isWalking", false);
        }

        // Store the vertical speed from the rigidbody
        verticalSpeed = rb.velocity.y;

        // Update timers
        jumpPressedTimer--;
        shootTimer--;
        recoveryTimer--;
        footstepTimer-= (int)Mathf.Abs(horizontalMovement * moveSpeed);

        // Play footstep SFX
        if (footstepTimer < 0 && IsGrounded()) {
            footstepTimer = footstepTime;
            PlayFootSound();
            GameObject wFX = Instantiate(walkingFX, new Vector3(transform.position.x, transform.position.y - 0.5f), new Quaternion());
            wFX.transform.Rotate(90, 0, 180);
            Destroy(wFX, 1);
        }

        // Jump buffering
        if (!IsGrounded()) {
            jumpBufferTimer--;
            
            // Set animation to jumping
            if (verticalSpeed > 0) {
                animator.SetBool("isJumping", true);
                animator.SetBool("isWalking", false);
            } 
            
            // Set animation to falling
            else {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                animator.SetBool("isWalking", false);
            }

        } else {
            if (jumpBufferTimer != jumpBufferTime) {
                // We're landing, this will fire once (ish?)
                xStretch = 2.4f;
                yStretch = .5f;

                PlayFootSound();
            }
            
            jumpBufferTimer = jumpBufferTime;
            availableJumps = MaxJumps;
        

            // Disable jump and fall flags to return to idle animation
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
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
        myRenderer.transform.localScale = new Vector3(xStretch, yStretch, 1);

        // Don't stretch the stapler, apply an inverse scale on it
       // stapler.transform.localScale = new Vector3(1 / xStretch, 1 / yStretch, 1);
        

        // Fall damage stuff
        if (wasGroundedLastFrame && !IsGrounded()) {
            startY = transform.position.y;
            //Debug.Log(startY);
        }

        if (!wasGroundedLastFrame && IsGrounded()) {

            if ((float)(startY - transform.position.y) > fallDamageDistance) {
                DoFallDamage();
            }
        }

        wasGroundedLastFrame = IsGrounded();

        myRenderer.enabled = true;
        if (isInHurtState) {
            myRenderer.enabled = (recoveryTimer % 4) > 2;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Projectile")) {
            Hurt(collision.gameObject.GetComponent<ProjectileBehavior>().ProjectileClass.damage);
        }
    }

    public void Hurt(int damageAmt) {
        if (recoveryTimer > recoveryTime)
            return;

        stats.DamageTaken(damageAmt);
        playerHurtSource.Play();
        recoveryTimer = recoveryTime;
    }

    private void DoFallDamage() {
        if (!takesFallDamage)
            return;

        Debug.Log("Fall damage taken!");
        
        Hurt(50);
    }
}
