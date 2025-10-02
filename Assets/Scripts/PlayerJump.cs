using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float jumpForce = 5;
    private float playerHalfHeight;

    private void Start() {
        playerHalfHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && GetIsGrounded())
        {
            Jump();
        }

        animator.SetBool("isJumping", !GetIsGrounded());
    }

    private bool GetIsGrounded() {
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }

    private void Jump() {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
