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

    // These variables are used for ray casting logic.
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

    // Function to handle jumping.
    private void Jump() {
        Vector2 jumpDirection = GetGroundNormal();
        rigidBody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    // These two functions play the jumping and landing sound effects;
    public void PlayJumpSFX() {
        SoundManager.instance.PlaySound(jumpClip);
    }

    public void PlayLandSFX() {
        SoundManager.instance.PlaySound(landClip);
    }

    // This functions checks whether the player is grounded. The player is only able to jump if they are grounded.
    public bool isGrounded() {
        groundHit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
        return groundHit.collider != null;
    }

    // This function is used to check whether the ground is flat or a slope.
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

    // Function to make the ray cast box visible in the editor.
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
