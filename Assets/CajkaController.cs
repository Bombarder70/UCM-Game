using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajkaController : MonoBehaviour {

	Transform pirat;

	private Animator cajkaAnimator;
	private Rigidbody rb;

	private Transform cajkaLetiaciBod;

	private bool flyAway = false;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 5f);
	}

	void Start() {
		this.rb = GetComponent<Rigidbody>();
		this.cajkaAnimator = GetComponent<Animator>();
		pirat = PlayerManager.instance.player.transform;

		var bod = Random.Range(1, 3); // RandomLetiaciBod
		this.cajkaLetiaciBod = GameObject.Find("CajkaLetiaciBod" + bod).transform;
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
			this.rb.velocity = transform.forward * 7;
		}
	}
}
