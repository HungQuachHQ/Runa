using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    private Animator animator;

    public float maxHealth;
    public float playerHealth;
    public float currentHealth;
    public Slider slider;

    public bool isHurt;
    public bool isDead;

    public float movementDelay = 0.3f;
    
    void Start() {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        playerHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    void Update() {
        HandleHealth();
    }

    private void HandleHealth() {
        if (currentHealth < playerHealth && !isDead) {
            playerHealth = currentHealth;
            slider.value = currentHealth;
            
            animator.SetTrigger("Attacked");
            isHurt = true;

            StartCoroutine(DelayMovement());
        }

        if (currentHealth <= 0) {
            animator.SetTrigger("Dead");
            isDead = true;
        }
        else {
            isDead = false;
        }
    }

    public void Heal(int healthRestore) {

    }

    private IEnumerator DelayMovement() {
        yield return new WaitForSeconds(movementDelay);
        isHurt = false;
    }
}
