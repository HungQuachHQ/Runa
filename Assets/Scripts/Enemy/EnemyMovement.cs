using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private Animator animator;

    private EnemyHealth healthStatus;
    private EnemyAttack attackStatus;

    public GameObject player;
    public float speed = 5f;
    public float distanceBetween;
    public float stopDistance;

    private float distance;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthStatus = GetComponent<EnemyHealth>();
        attackStatus = GetComponent<EnemyAttack>();
    }

    void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        float directionX = player.transform.position.x - transform.position.x;

        if ((distance < distanceBetween) && canMove()) {
            if (Mathf.Abs(directionX) > stopDistance) {
                float moveDirection = Mathf.Sign(directionX);
                rigidBody.velocity = new Vector2(moveDirection * speed, rigidBody.velocity.y);

                animator.SetBool("isWalking", true);

                // Flip sprite according to movement direction
                transform.localScale = new Vector3(moveDirection, 1, 1);
            }
            else {
                // If Player is within stop distance, stop horizontal movement while keeping direction
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                animator.SetBool("isWalking", false);
            }
        }
        else {
            // Stop moving if player is out of range
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            animator.SetBool("isWalking", false);
        }
    }

    // Stop moving if attacked or dead, or is attacking
    private bool canMove() {
        if (healthStatus.isDead || healthStatus.isHurt || attackStatus.isAttacking) {
            return false;
        }
        else {
            return true;
        }
    }
}
