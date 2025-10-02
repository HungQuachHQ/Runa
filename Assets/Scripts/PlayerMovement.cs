using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private float horizontalInput;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    private const float DOUBLE_TAP_TIME = .2f;
    private float lastTapTime;
    private KeyCode lastDirectionKey;

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

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rigidBody.velocity = new Vector2(horizontalInput * currentSpeed, rigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KeyCode thisKey = Input.GetKeyDown(KeyCode.RightArrow) ? KeyCode.RightArrow : KeyCode.LeftArrow;
            float timeSinceLastTap = Time.time - lastTapTime;

            if (lastDirectionKey == thisKey && timeSinceLastTap <= DOUBLE_TAP_TIME)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            lastTapTime = Time.time;
            lastDirectionKey = thisKey;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isRunning = false;
        }
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
