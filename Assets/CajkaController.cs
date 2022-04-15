using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajkaController : MonoBehaviour {

	Transform pirat;

	public Animator cajkaAnimator;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 5f);
	}

	void Start() {
		pirat = PlayerManager.instance.player.transform;
	}

	void Update() {
		float distance = Vector3.Distance(pirat.position, transform.position);

		if (distance < 3) {
			cajkaAnimator.SetBool("flyAway", true);
		}
	}
}
