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


    // UPGRADE STATS
    public int ShootTime = 5; // frames that need to pass between each gun fire
    public int MaxJumps = 1;
    [SerializeField] public float moveSpeed = 12f; // top speed
    [SerializeField] public float acceleration = 2f; // acceleration to top speed
    [SerializeField] public float jumpSpeed = 3f;




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
            float attackAngle = (isFacingRight) ? 0 : Mathf.PI;
            stapler.FireStaple(attackAngle);
            shootTimer = ShootTime;
        }
    }

    private void FixedUpdate() {
        // update the direction
        if (Mathf.Abs(horizontalMovement) > 0) {
            isFacingRight = (Mathf.Sign(horizontalMovement) > 0);
            myRenderer.flipX = !isFacingRight;
            stapler.VisualUpdate(isFacingRight);
        }

        verticalSpeed = rb.velocity.y;
        jumpPressedTimer--;
        shootTimer--;
        footstepTimer-= (int)Mathf.Abs(horizontalMovement * moveSpeed);

        if (footstepTimer < 0 && IsGrounded()) {
            footstepTimer = footstepTime;
            walkingSource.clip = walkingSounds[UnityEngine.Random.Range(0, walkingSounds.Length)];
            walkingSource.Play();
        }

        //Debug.Log(jumpBufferTimer);
        if (!IsGrounded()) {
            jumpBufferTimer--;
        } else {
            jumpBufferTimer = jumpBufferTime;
            availableJumps = MaxJumps;
        }

        // Redefine acceleration so we can modify it mid run
        float accel = acceleration;
        
        if (Mathf.Sign(horizontalMovement) != Mathf.Sign(horizontalSpeed)) {
            if (IsGrounded()) {
                accel *= 0.95f;
            } else {
                accel *= .75f;
            }
        }



        horizontalSpeed += horizontalMovement * accel;

        if (Mathf.Abs(horizontalMovement) < 0.1f) {
            horizontalSpeed *= 0.595f;
        }

        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -moveSpeed, moveSpeed);

        // Jump
        if (CanJump() && jumpPressedTimer > 0) {
            verticalSpeed = jumpSpeed;
            availableJumps--;
            jumpPressedTimer = 0;
            jumpBufferTimer = 0;

            playerJumpSource.Play();
        }

        if (!jumpPressed && verticalSpeed > 0) {
            verticalSpeed *= 0.875f;
        }

        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }
}
