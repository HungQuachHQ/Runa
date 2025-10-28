using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 5;

    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip landClip;

    public bool grounded;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private RaycastHit2D groundHit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded()) {
            Jump();
            animator.SetBool("isJumping", true);
        }
    }

    private void Jump() {
        Vector2 jumpDirection = GetGroundNormal();
        rigidBody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    public void PlayJumpSFX() {
        SoundManager.instance.PlaySound(jumpClip);
    }

    public void PlayLandSFX() {
        SoundManager.instance.PlaySound(landClip);
    }

    public bool isGrounded() {
        groundHit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
        return groundHit.collider != null;
    }

    public Vector2 GetGroundNormal() {
        if (groundHit.collider != null) {
            return groundHit.normal;
        }
        else {
            return Vector2.up;
        }
    }

    // Event function to trigger when the player has landed
    public void OnLanding () {
        animator.SetBool("isJumping", false);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
