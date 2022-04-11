using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour {

	public Animator enemyAnimator;
	private Animator playerAnimator;

	void Start() {
		GameObject pirat = GameObject.FindWithTag("pirat");
 
		if (pirat != null) {
			Debug.Log(pirat);
			this.playerAnimator = pirat.GetComponent<Animator>();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "sword") {
			if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
				Debug.Log(1);
				enemyAnimator.SetBool("getHit", true);
			}
		}
	}

}
