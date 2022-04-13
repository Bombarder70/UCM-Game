using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour {

	public Animator enemyAnimator;
	private Animator playerAnimator;

	private bool stopEnemyDamage = false;

	private int health;

	void Start() {
		GameObject pirat = GameObject.FindWithTag("pirat");
		
		switch (gameObject.tag) {
			case "normal_skeleton":
				this.health = 2;
			break;
			case "green_eyes_skeleton":
				this.health = 4;
			break;
				case "red_eyes_skeleton":
				this.health = 6;
			break;
		}
 
		if (pirat != null) {
			this.playerAnimator = pirat.GetComponent<Animator>();
		}
	}

	void Update() {
		if (this.animatorIsPlaying("getHitAnimation")) 	enemyAnimator.SetBool("getHit", false);

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
			if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !this.stopEnemyDamage) {
				enemyAnimator.SetBool("getHit", true);

				this.stopEnemyDamage = true;
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
