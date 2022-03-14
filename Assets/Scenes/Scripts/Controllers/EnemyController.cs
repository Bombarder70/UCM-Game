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

    public int damageIteration = 1;

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

            enemyAnimator.Play("Attack");

            if (this.getAnimationName("Attack")) {
                if (this.getAnimationTime() > 0.5 * this.damageIteration) {
                    this.damageIteration++;
                    //Score.changeScore(-10);
                }
            }
        } else if (distance <= enemyLook) {
            enemyAnimator.SetBool("isRunning", true);
            enemyAnimator.SetBool("isAttacking", false);
            agent.SetDestination(target.position);
            this.damageIteration = 1;
        } else {
            enemyAnimator.SetBool("isRunning", false);
            this.damageIteration = 1;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyLook);
    }

    double getAnimationTime() {
      return enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool getAnimationName(string name) {
      return enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
