using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    private Animator animator;

    public float health;
    public float currentHealth;
    public Slider slider;

    public bool isHurt;
    public bool isDead;

    public float movementDelay = 0.3f;
    
    void Start() {
        animator = GetComponent<Animator>();
        currentHealth = health;
        slider.maxValue = health;
        slider.value = currentHealth;
    }

    void Update() {
        HandleHealth();
    }

    private void HandleHealth() {
        if (health < currentHealth && !isDead) {
            currentHealth = health;
            slider.value = currentHealth;
            
            animator.SetTrigger("Attacked");
            isHurt = true;

            StartCoroutine(DelayMovement());
        }

        if (health <= 0) {
            animator.SetTrigger("Dead");
            isDead = true;
        }
        else {
            isDead = false;
        }
    }

    private IEnumerator DelayMovement() {
        yield return new WaitForSeconds(movementDelay);
        isHurt = false;
    }
}
