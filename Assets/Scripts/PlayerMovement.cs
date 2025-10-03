using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private float horizontalInput;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    private bool isRunning;
    private bool isFacingLeft = false;

    private void Update()
    {
        HandleMovement();
        FlipSprite();
        Animate();
    }

    private void Animate() {
        if (horizontalInput != 0 && !isRunning)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetBool("isRunning", isRunning);
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Checking for running (holding left shift + left/right arrow keys)
        bool isShiftHeld = Input.GetKey(KeyCode.LeftShift);
        bool isMovingHorizontally = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        isRunning = isShiftHeld && isMovingHorizontally;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rigidBody.velocity = new Vector2(horizontalInput * currentSpeed, rigidBody.velocity.y);
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
}
