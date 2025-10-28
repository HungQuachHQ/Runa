using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private float horizontalInput;

    // Default player's walk and run speed.
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    private bool isRunning;
    private bool isFacingLeft = false;

    // These variables are for movement blocking purposes.
    private PlayerAttack moveStatus;
    private PlayerHealth healthStatus;
    private PlayerJump groundStatus;

    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip runClip;

    void Start() {
        moveStatus = GetComponent<PlayerAttack>();
        healthStatus = GetComponent<PlayerHealth>();
        groundStatus = GetComponent<PlayerJump>();
    }

    void Update() {
        HandleMovement();
        FlipSprite();
        Animate();
    }

    // Function to handle animation
    private void Animate() {
        if (horizontalInput != 0 && !isRunning) {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }

        animator.SetBool("isRunning", isRunning);
    }

    // Function to handle movement
    private void HandleMovement() {
        if (canMove()) {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            // Checking for running (holding left shift + left/right arrow keys)
            bool isShiftHeld = Input.GetKey(KeyCode.LeftShift);
            bool isMovingHorizontally = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
            isRunning = isShiftHeld && isMovingHorizontally;

            // Determining which speed to use when moving
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            // Determing whether the ground is flat or a slope
            Vector2 groundNormal = groundStatus.GetGroundNormal();
            float slopeAngle = Vector2.Angle(groundNormal, Vector2.up);

            if (slopeAngle <= 45) {
                // Handling slopes logic
                Vector2 perp = Vector2.Perpendicular(groundNormal).normalized;
                float directionSign = Mathf.Sign(Vector2.Dot(perp, Vector2.right));
                Vector2 moveDirection = perp * directionSign * horizontalInput;

                rigidBody.velocity = new Vector2(moveDirection.x * currentSpeed, rigidBody.velocity.y);
            }
            else {
                rigidBody.velocity = new Vector2(horizontalInput * currentSpeed, rigidBody.velocity.y);
            }
        }
    }

    // These two functions are used to play the walk and run sound effects.
    public void PlayWalkSFX() {
        SoundManager.instance.PlaySound(walkClip);
    }

    public void PlayRunSFX() {
        SoundManager.instance.PlaySound(runClip);
    }

    // Function to flip the sprite when turning left or right
    void FlipSprite() {
        if (isFacingLeft && horizontalInput > 0f || !isFacingLeft && horizontalInput < 0f) {
            isFacingLeft = !isFacingLeft;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    // This determines whether the player can move.
    private bool canMove() {
        if (moveStatus.movementBlocked || healthStatus.isHurt || healthStatus.isDead) {
            return false;
        }
        else {
            return true;
        }
    }
}
