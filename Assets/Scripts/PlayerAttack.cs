using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] Animator animator;

    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    public float delay = 0.3f;
    private bool attackedBlocked;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }

    public void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    // Attack_1 radius
    public void Attack_1() {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        
        foreach (Collider2D enemyGameObject in enemy) {
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    public void Attack()
    {
        if (attackedBlocked) {
            return;
        }

        animator.SetBool("isAttacking", true);
        attackedBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackedBlocked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
