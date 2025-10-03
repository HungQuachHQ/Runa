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
        }

        animator.SetBool("isJumping", !isGrounded());
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        Vector3 normal = collision.GetContact(0).normal;
    //        if (normal == Vector3.up)
    //        {
    //            grounded = true;
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        grounded = false;
    //    }
    //}
}
