using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] Animator animator;

    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("isAttacking", true);
        }
    }

    public void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy)
        {
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
