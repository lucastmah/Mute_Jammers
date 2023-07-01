using System.Collections;
using System.Collections.Generic;
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

    bool isFacingRight;

    [SerializeField] private PlayerStats stats;

    // UPGRADE STATS
    public int ShootTime = 5; // frames that need to pass between each gun fire
    public int MaxJumps = 1;
    [SerializeField] public float moveSpeed = 12f; // top speed
    [SerializeField] public float acceleration = 2f; // acceleration to top speed
    [SerializeField] public float jumpSpeed = 3f;

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
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        

        if (Input.GetButtonDown("Jump")) {
            jumpPressedTimer = 20;
        }

        jumpPressed = Input.GetButton("Jump");

        if (Input.GetButtonDown("Fire1") && shootTimer < 0) {
            int attackAngle = (isFacingRight) ? 1 : -1;
            stapler.FireStaple(attackAngle);
            shootTimer = ShootTime;
        }
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
            walkingSource.clip = walkingSounds[UnityEngine.Random.Range(0, walkingSounds.Length)];
            walkingSource.Play();
        }

        // Jump buffering
        if (!IsGrounded()) {
            jumpBufferTimer--;
        } else {
            if (jumpBufferTimer != jumpBufferTime) {
                // We're landing, this will fire once (ish?)
                xStretch = 2.4f;
                yStretch = .5f;
            }
            
            jumpBufferTimer = jumpBufferTime;
            availableJumps = MaxJumps;

            
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
        xStretch = Mathf.Lerp(xStretch, 1, 0.1f);
        yStretch = Mathf.Lerp(yStretch, 1, 0.1f);

        if (xStretch > 0.95f) {
            xStretch = 1;
        }

        if (yStretch > 0.95f) {
            yStretch = 1;
        }

        myRenderer.transform.transform.localScale = new Vector3(xStretch, yStretch, 1);

    }
}
