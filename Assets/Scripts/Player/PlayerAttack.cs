using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip lightClip;

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

    public void endAttack() {
        animator.SetBool("isAttacking", false);
    }

    // Attack_1 radius
    public void Attack_1() {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint1.transform.position, radius, enemies);
        
        foreach (Collider2D enemyGameObject in enemy) {
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

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

    public void PlayLightSFX() {
        SoundManager.instance.PlaySound(lightClip);
    }

    private IEnumerator DelayMovement() {
        yield return new WaitForSeconds(movementDelay);
        movementBlocked = false;
    }

    private IEnumerator DelayAttack() {
        yield return new WaitForSeconds(attackDelay);
        attackedBlocked = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackPoint1.transform.position, radius);
    }
}
