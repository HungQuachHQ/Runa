using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float distanceBetween;

    private float distance;
    private Rigidbody2D rigidBody;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        float directionX = player.transform.position.x - transform.position.x;

        if (distance < distanceBetween) {
            if (Mathf.Abs(directionX) > 0.1f) // Threshold to avoid flipping when aligned
            {
                float moveDirection = Mathf.Sign(directionX);
                rigidBody.velocity = new Vector2(moveDirection * speed, rigidBody.velocity.y);

                animator.SetBool("isWalking", true);

                // Flip sprite according to movement direction
                transform.localScale = new Vector3(moveDirection, 1, 1);
            }
            else
            {
                // Player is very close or aligned, stop horizontal movement but keep facing direction
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
}
