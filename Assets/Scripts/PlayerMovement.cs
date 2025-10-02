using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D body;

    private float Move;

    public float speed;
    public float jump;

    bool grounded;
    private Animator animate;

    private void Start() {
        // Grabs references for the RigidBody and Animator from object
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    private void Update() {
        Move = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Move * speed, body.velocity.y);

        // Flips player wheb move left and right
        if (Move > 0.01f) {
            transform.localScale = Vector3.one;
        }
        else if (Move < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.UpArrow) && grounded) {
            Jump();
        }

        // Setting Animator parameters
        animate.SetBool("Walk", Move != 0);
        animate.SetBool("Grounded", grounded);
    }

    private void Jump() {
        body.velocity = new Vector2(body.velocity.x, jump);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            Vector3 normal = collision.GetContact(0).normal;
            if (normal == Vector3.up) {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
