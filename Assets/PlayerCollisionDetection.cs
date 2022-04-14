using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {

  void OnTriggerEnter(Collider other) {
		Debug.Log(other);
	}

}
