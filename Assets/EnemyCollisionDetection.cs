using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour {

	public Animator enemyAnimator;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "sword") {
			enemyAnimator.SetBool("getHit", true);
		}
	}

}
