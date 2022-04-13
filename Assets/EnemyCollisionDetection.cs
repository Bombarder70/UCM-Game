using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour {

	public Animator enemyAnimator;
	private Animator playerAnimator;

	private bool stopEnemyDamage = false;

	public int health;

	void Start() {
		GameObject pirat = GameObject.FindWithTag("pirat");
		this.health = gameObject.GetComponent<EnemyController>().health;
 
		if (pirat != null) {
			this.playerAnimator = pirat.GetComponent<Animator>();
		}
	}

	void Update() {
		if (this.animatorIsPlaying("getHitAnimation")) {
			if (this.stopEnemyDamage == false) {
				this.stopEnemyDamage = true;
				this.health -= 1;
				Debug.Log(this.health);
			}
		} else {
			this.stopEnemyDamage = false;
		}

		if (this.health == 0 && this.getAnimationTime() > 0.1) {
			gameObject.GetComponent<EnemyController>().die();
		}

	}

  // Sword hit
	void OnTriggerEnter(Collider other) {
		if (other.tag == "sword") {
			if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
				enemyAnimator.Play("getHitAnimation");
			}
		}
	}

	double getAnimationTime() {
		return enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	bool getAnimationName(string name) {
		return enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
	}

	bool animatorIsPlayingTime(){
		return enemyAnimator.GetCurrentAnimatorStateInfo(0).length > enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	bool animatorIsPlaying(string animationName){
		return animatorIsPlayingTime() && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
	}	

}
