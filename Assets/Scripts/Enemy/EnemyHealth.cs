using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    private Animator animator;

    public float health;
    public float currentHealth;

    public bool isHurt;
    public bool isDead;

    public float movementDelay = 0.3f;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    void Update() {
        HandleHealth();
    }

    private void HandleHealth() {
        if (health < currentHealth && !isDead) {
            currentHealth = health;
            animator.SetTrigger("Attacked");
            isHurt = true;

            StartCoroutine(DelayMovement());
        }

        if (health <= 0) {
            animator.SetBool("isDead", true);
            isDead = true;
        }
        else {
            isDead = false;
        }
    }

    private IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(movementDelay);
        isHurt = false;
    }
}
