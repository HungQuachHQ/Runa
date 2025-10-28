using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip lightClip;

    // These variables are used to determine the range of the attack.
    public GameObject attackPoint1;
    public float radius;
    public LayerMask enemies;
    public float damage;

    public float attackDelay = 0.3f;
    public float movementDelay = 0.3f;
    
    public bool attackedBlocked;
    public bool movementBlocked;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            Attack();
        }
    }

    // This function is used to stop the attacking animation.
    public void endAttack() {
        animator.SetBool("isAttacking", false);
    }

    // This checks whether the attack collides with the target. If so, damage is dealt to the target.
    public void Attack_1() {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint1.transform.position, radius, enemies);
        
        foreach (Collider2D enemyGameObject in enemy) {
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    // This handles the attack animation, attack delay, and movement delay.
    public void Attack() {
        if (attackedBlocked) {
            return;
        }
        
        movementBlocked = true;
        animator.SetBool("isAttacking", true);
        attackedBlocked = true;
        
        StartCoroutine(DelayAttack());
        StartCoroutine(DelayMovement());
    }

    // This plays the attack sound effect of the player.
    public void PlayLightSFX() {
        SoundManager.instance.PlaySound(lightClip);
    }

    // Function for delaying the movement.
    private IEnumerator DelayMovement() {
        yield return new WaitForSeconds(movementDelay);
        movementBlocked = false;
    }

    // Function for delaying the attack.
    private IEnumerator DelayAttack() {
        yield return new WaitForSeconds(attackDelay);
        attackedBlocked = false;
    }

    // Function to make the range of the attack visible in the editor.
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackPoint1.transform.position, radius);
    }
}
