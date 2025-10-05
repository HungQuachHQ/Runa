using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    [SerializeField] private Animator animator;
    public GameObject player;
    
    public GameObject attackPoint1;
    public float radius;
    public LayerMask players;
    public float damage;

    public float attackRange;
    public float attackDelay = 0.3f;

    public bool isAttacking;
    private bool attackBlocked;

    void Update() {
        AttackRange();
    }

    private void AttackRange() {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        float directionX = player.transform.position.x - transform.position.x;
        float playerDirection = Mathf.Sign(directionX);
        float enemyDirection = Mathf.Sign(transform.localScale.x);

        if (distance <= attackRange) {
            if (!isAttacking && playerDirection != enemyDirection) {
                transform.localScale = new Vector3(playerDirection, 1, 1);
            }

            Attack();
        }
    }

    private void Attack() {
        if (attackBlocked) {
            return;
        }

        isAttacking = true;
        animator.SetTrigger("Attack_1");
        attackBlocked = true;

        StartCoroutine(DelayAttack());
    }

    public void Attack_1() {
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint1.transform.position, radius, players);

        foreach (Collider2D playerGameObject in player) {
            playerGameObject.GetComponent<PlayerHealth>().health -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint1.transform.position, radius);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
        isAttacking = false;
    }
}
