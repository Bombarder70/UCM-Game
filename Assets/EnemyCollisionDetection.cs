using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour {

	public Animator enemyAnimator;
	private Animator playerAnimator;

	private float attackTimeout = 1;
	private bool stopEnemyDamage = false;

	public int health;

	void Start() {
		GameObject pirat = GameObject.FindWithTag("pirat");
		this.health = gameObject.GetComponent<EnemyController>().health;
 
		if (pirat != null) {
			Debug.Log(pirat);
			this.playerAnimator = pirat.GetComponent<Animator>();
		}
	}

	void Update() {
		if (!this.stopEnemyDamage && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("getHitAnimation")) {
			if (getAnimationTime() > 0.5) {
				this.health -= 1;
				this.stopEnemyDamage = true;
				Debug.Log(this.health);
			}
		}

		if (this.health == 0) {
			gameObject.GetComponent<EnemyController>().die();
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "sword") {
			if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
				enemyAnimator.Play("getHitAnimation");
				this.stopEnemyDamage = false;
			}
		}
	}

	double getAnimationTime() {
		return enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	bool getAnimationName(string name) {
		return enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
	}

}
