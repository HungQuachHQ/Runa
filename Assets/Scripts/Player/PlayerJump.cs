using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 5;

    public bool grounded;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded()) {
            Jump();
            animator.SetBool("isJumping", true);
        }
    }

    private void Jump() {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool isGrounded() {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    // Event function to trigger when the player has landed
    public void OnLanding () {
        animator.SetBool("isJumping", false);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
