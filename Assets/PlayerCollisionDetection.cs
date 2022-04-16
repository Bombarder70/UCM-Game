using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {

  void OnTriggerEnter(Collider other) {
	  	if (other.tag == "Lava")
		  {
			  Movement.playerIsDead = true;
			  GameManager.Instance.GameOver();
			
		  }
		Debug.Log(other);
	}

}
