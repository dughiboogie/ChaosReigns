using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    private new Rigidbody2D rigidbody;  // Movement is set with rigidbody.velocity
    private SpriteRenderer spriteRenderer;
    //private Animator animator;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform cameraTarget;

    // Movement variables
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float hangTime = .05f;         // Hang time (coyote effect)
    private float hangCounter;
    [SerializeField] private float jumpBufferTime = .1f;    // Expanded time slot to let the player jump before landing
    private float jumpBufferCounter;

    // Camera movement variables
    private float moveHorizontal;       // Horizontal axis (left stick) value
    public int facingDirection = 1;    // Facing direction of the player (1: right, -1: left)

    private float groundCheckCircleRadius;  // Radius of CircleCollider2D that checks if player is grounded
    public bool isGrounded;

    private bool wasOnGround = true;   // Last frame's isGrounded value

    // Powers variables
    private PlayerPowers powers;
    private int maxAirJump = 1;     // Double jump
    private int airJumpCount = 0;
    private int maxAirDash = 1;     // Dash
    private int airDashCount = 0;
    public float dashValue = 4000f;
    public float dashCooldown = 1f;
    private float dashCooldownCounter;
    public Transform wallCheck;     // Wall slide/jump
    private float wallCheckCircleRadius;
    private bool isInContactWithWall;
    public float xWallJumpForce = 1000f;
    public PhysicsMaterial2D playerMaterial, wallSlideMaterial;
    private float wallHangTime = .1f;
    private float wallHangCounter;
    private float wallJumpBufferTime = .1f;
    private float wallJumpBufferCounter;

    // Invert keys effect
    public bool invertKeys = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        groundCheckCircleRadius = groundCheck.GetComponent<CircleCollider2D>().radius;
        wallCheckCircleRadius = wallCheck.GetComponent<CircleCollider2D>().radius;

        powers = GetComponent<PlayerPowers>();

        dashCooldownCounter = 0f;
    }

    private void Update()
    {
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckCircleRadius, whatIsGround);  // Check every frame if player is grounded
        //animator.SetBool("Grounded", isGrounded);

        isInContactWithWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckCircleRadius, whatIsGround);

        if(!isGrounded && isInContactWithWall && powers.currentPower.name == "Wall jump") {
            rigidbody.sharedMaterial = wallSlideMaterial;
        }
        else {
            rigidbody.sharedMaterial = playerMaterial;
        }

        // Dash variables
        if(isGrounded) {
            airDashCount = 0;
            if(!wasOnGround) {
                dashCooldownCounter = 0;
            }
        }
        if(dashCooldownCounter > 0) {
            dashCooldownCounter -= Time.deltaTime;
        }

        // Reset camera target position if player is moving or not grounded
        if(moveHorizontal != 0 || !isGrounded) {
            cameraTarget.localPosition = Vector2.zero;
        }

        #region Jumps

        // Manage hang time (Coyote Effect)
        if(isGrounded) {
            hangCounter = hangTime;
            airJumpCount = 0;
        }
        else {
            hangCounter -= Time.deltaTime;
        }

        // Manage jump buffer counter
        if(jumpBufferCounter > 0) {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Manage jumps
        if(jumpBufferCounter > 0 && hangCounter > 0) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            jumpBufferCounter = 0;

            // When player jumps set dash cooldown counter to 0, to make him able to dash right away
            dashCooldownCounter = 0;

            // AudioManager.instance.Play("Player_Jump");
        }

        #endregion

        #region WallJumps

        if(isInContactWithWall) {
            wallHangCounter = wallHangTime;
        }
        else {
            wallHangCounter -= Time.deltaTime;
        }

        if(wallJumpBufferCounter > 0) {
            wallJumpBufferCounter -= Time.deltaTime;
        }

        if(wallJumpBufferCounter > 0 && wallHangCounter > 0) {
            rigidbody.velocity = new Vector2(0, jumpForce);
            rigidbody.AddForce(new Vector2(xWallJumpForce * (-facingDirection), 0));

            wallJumpBufferCounter = 0;
        }

        #endregion

        // Manage player horizontal velocity (mantain y velocity and modify x velocity)
        /*
        if(rigidbody.velocity.y < 0f && !isGrounded) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 1.01f);
        }
        */

        rigidbody.velocity = new Vector2(moveHorizontal * movementSpeed, rigidbody.velocity.y);
        
        // Manage animator variables
        //animator.SetFloat("Horizontal speed", Mathf.Abs(moveHorizontal));
        //animator.SetFloat("Y velocity", rigidbody.velocity.y);
        
        wasOnGround = isGrounded;   // wasOnGround is the last frame's grounded status

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<Vector2>().x;    // moveHorizontal changes value on every input on the horizontal axis

        if(invertKeys == true) {
            moveHorizontal *= -1;
        }

        if(moveHorizontal > -0.4f && moveHorizontal < 0.4f)
            moveHorizontal = 0;

        if(moveHorizontal > 0) {
            if(facingDirection == -1) {
                transform.Rotate(0, 180, 0);
                facingDirection = 1;
            }
        }
        else if(moveHorizontal < 0) {
            if(facingDirection == 1) {
                transform.Rotate(0, 180, 0);
                facingDirection = -1;
            }
        }
    }

    /**
     * The jump button interaction is the default interaction. 
     * When the button is pressed, the action is performed, when it's released the action is canceled.
     * If the player's velocity is positive and the action is canceled, the vertical velocity is decreased to make smaller jumps.
     * 
     * OLD REMINDER: If the TapInteraction is started but not performed, it is automatically canceled.
     */

    public void Jump(InputAction.CallbackContext context)
    {
        // Reset jump buffer counter
        if(context.performed) {
            jumpBufferCounter = jumpBufferTime;

            // Double jump
            if(hangCounter <= 0 && powers.currentPower.name == "Double jump") {
                if(airJumpCount < maxAirJump) {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                    airJumpCount++;
                }
            }

            // Wall jump
            if(powers.currentPower.name == "Wall jump") {
                wallJumpBufferCounter = wallJumpBufferTime;
            }

        }

        // Small jumps based on jump button release
        if(context.canceled && rigidbody.velocity.y > 0) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * .3f);
        }

    }

    /**
     * Thrust player forward, with a cooldown for the next time he can dash. If player is in the air he can dash only one time.
     */
    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && powers.currentPower.name == "Dash") {
            if(dashCooldownCounter <= 0) {
                if(isGrounded) {
                    dashCooldownCounter = dashCooldown;

                    rigidbody.AddForce(new Vector2(dashValue * facingDirection, 0f));
                    //animator.SetTrigger("Dash");
                }

                if(!isGrounded && airDashCount < maxAirDash) {
                    dashCooldownCounter = dashCooldown;
                    airDashCount++;

                    rigidbody.AddForce(new Vector2(dashValue * facingDirection, 0f));
                    //animator.SetTrigger("Dash");
                }
            }
        }
    }

    public void UpThrust(float upwardThrustForce)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, upwardThrustForce);
    }

}