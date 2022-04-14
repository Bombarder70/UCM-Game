using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollect : MonoBehaviour{
    
	public GameObject Heart;

	void Update(){
		transform.Rotate (0, 0, 2);
	}

	void OnTriggerEnter () {
		if (HealthMonitor.HealthValue != 3) {
			HealthMonitor.HealthValue += 1;
			Heart.SetActive (false);
		}
	}

}
