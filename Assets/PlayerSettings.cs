using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {

	public GameObject getHitScreen;
	public Color getHitScreenColor;

	public void getHit() {
		var color = getHitScreen.GetComponent<Image>().color;
		color.a = 0.5f;

		getHitScreen.GetComponent<Image>().color = color;

		//if (HealthMonitor.HealthValue > 1) gameObject.GetComponent<Animator>().Play("getHit");
	}

  void Update() {
		if (getHitScreen.GetComponent<Image>().color.a > 0) {
			var color = getHitScreen.GetComponent<Image>().color;
			color.a -= 0.01f;
			getHitScreen.GetComponent<Image>().color = color;
		}
	}

}
