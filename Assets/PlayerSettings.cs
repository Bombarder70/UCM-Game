using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {

	public GameObject getHitScreen;
	public Color getHitScreenColor;
	private Animator playerAnimator;

	public void Start() {
		this.playerAnimator = gameObject.GetComponent<Animator>();
	}

	public void getHit() {
		var color = getHitScreen.GetComponent<Image>().color;
		color.a = 0.5f;

		getHitScreen.GetComponent<Image>().color = color;

		this.playerAnimator.SetBool("getHit", true);
		//if (HealthMonitor.HealthValue > 1) gameObject.GetComponent<Animator>().Play("getHit");
	}

  void Update() {
		if (getHitScreen.GetComponent<Image>().color.a > 0) {
			var color = getHitScreen.GetComponent<Image>().color;
			color.a -= 0.01f;
			getHitScreen.GetComponent<Image>().color = color;
		}

		if (this.animatorIsPlaying("getHit")) this.playerAnimator.SetBool("getHit", false);
		if (this.animatorIsPlayingInBase("getHit")) this.playerAnimator.SetBool("getHit", false);
	}

	bool animatorIsPlayingTime() {
		return playerAnimator.GetCurrentAnimatorStateInfo(2).length > playerAnimator.GetCurrentAnimatorStateInfo(2).normalizedTime;
	}

	bool animatorIsPlaying(string animationName) {
		return animatorIsPlayingTime() && playerAnimator.GetCurrentAnimatorStateInfo(2).IsName(animationName);
	}	

	bool animatorIsPlayingTimeInBase() {
		return playerAnimator.GetCurrentAnimatorStateInfo(0).length > playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	bool animatorIsPlayingInBase(string animationName) {
		return animatorIsPlayingTimeInBase() && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
	}	

}
