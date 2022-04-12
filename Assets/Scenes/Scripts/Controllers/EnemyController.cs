using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyLook = 10f;
    public Animator enemyAnimator;

		public int health = 5;
    
    Transform target;
    NavMeshAgent agent;

    public int damageIteration = 1;

		private bool stopEnemyMoving = false;
		private int randomPlayerDeadAnimation = 0;

    void Start() {
			target = PlayerManager.instance.player.transform;
			agent = GetComponent<NavMeshAgent>();

			enemyAnimator = GetComponent<Animator>();

			setRigidbodyState(true);
			setColliderState(false);
			GetComponent<Animator>().enabled = true;
    }

    public void die() {
			GetComponent<Animator>().enabled = false;
			setRigidbodyState(false);
			setColliderState(true);

			agent.enabled = false;
			this.stopEnemyMoving = true;

			if (gameObject != null) {
				Destroy(gameObject, 15f);
			}
    }

    void Update() {

			/**
			  * Ak enemydostane hit tak za nemoze hybat
				* Ak je hrac mrtvy tak sa enemy nehybe
			 */
			if (this.getAnimationName("getHitAnimation") || Movement.playerIsDead) this.stopEnemyMoving = true;
			else this.stopEnemyMoving = false;

			if (this.stopEnemyMoving == false) {
				float distance = Vector3.Distance(target.position, transform.position);

				if (distance < 1.5) {
					enemyAnimator.SetBool("isRunning", false);
					enemyAnimator.SetBool("isAttacking", true);

					//enemyAnimator.Play("Attack");

					if (this.getAnimationName("Attack")) {
						if (this.getAnimationTime() > 0.7 * this.damageIteration) {
							this.damageIteration++;

							if (Score.score > 0) {
								Score.score -= 10;
							} else {
								if (HealthMonitor.HealthValue > 0) {
									HealthMonitor.HealthValue -= 1;
								}
							}
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
			} else {
				enemyAnimator.SetBool("isAttacking", false);

				this.getRandomPlayerDeadAnimation();
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

		void setRigidbodyState(bool state) {
			Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

			foreach (Rigidbody rigidbody in rigidbodies){
				rigidbody.isKinematic = state;
			}

			GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state) {
			Collider[] colliders = GetComponentsInChildren<Collider>();

			foreach (Collider collider in colliders) {
				collider.enabled = state;
			}

			GetComponent<Collider>().enabled = !state;
    }

		void getRandomPlayerDeadAnimation() {
			if (this.randomPlayerDeadAnimation < 1) {
				this.randomPlayerDeadAnimation = Random.Range(0, 4);
				if (this.randomPlayerDeadAnimation == 1) 	enemyAnimator.SetBool("scream", true);
			}

			if (this.animatorIsPlaying("Scream")) enemyAnimator.SetBool("scream", false);
		}

		bool animatorIsPlayingTime() {
		  return enemyAnimator.GetCurrentAnimatorStateInfo(0).length > enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	  }

    bool animatorIsPlaying(string animationName) {
      return animatorIsPlayingTime() && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }	
}
