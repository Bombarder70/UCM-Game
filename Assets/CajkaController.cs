using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajkaController : MonoBehaviour {

	Transform pirat;

	public Animator cajkaAnimator;
	private Rigidbody rb;

	private Transform cajkaLetiaciBod;

	private bool flyAway = false;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 5f);
	}

	void Start() {
		this.rb = GetComponent<Rigidbody>();
		pirat = PlayerManager.instance.player.transform;

		this.cajkaLetiaciBod = GameObject.Find("CajkaLetiaciBod").transform;
	}

	void Update() {
		float distance = Vector3.Distance(pirat.position, transform.position);

		if (this.flyAway == false) {
			if (distance < 3) {
				cajkaAnimator.SetBool("flyAway", true);
				transform.LookAt(cajkaLetiaciBod);
				this.flyAway = true;
			}
		} else {
			this.rb.velocity = transform.forward * 5;
		}
	}
}
