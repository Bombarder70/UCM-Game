using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController {

	public class Main : MonoBehaviour {

		void Start() {

		}


		void Update() {
				
		}

	}

	public class Keyboard {

		public void getSettings() {
			Debug.Log("Keyboard setttings");
		}

		public void checkEscapePress() {
			Debug.Log("Escape");
			if (Input.GetKeyDown ("escape")) {
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}

}
