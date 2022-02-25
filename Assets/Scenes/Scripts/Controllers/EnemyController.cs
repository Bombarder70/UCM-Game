using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyLook = 10f;
    public Animator enemyAnimator;
    
    Transform target;
    NavMeshAgent agent;

    void Start() {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        enemyAnimator = GetComponent<Animator>();
    }


    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < 1.5) {
            enemyAnimator.SetBool("isRunning", false);
            enemyAnimator.SetBool("isAttacking", true);
        } else if (distance <= enemyLook) {
            enemyAnimator.SetBool("isRunning", true);
            enemyAnimator.SetBool("isAttacking", false);
            agent.SetDestination(target.position);
        } else {
            enemyAnimator.SetBool("isRunning", false);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyLook);
    }
}
