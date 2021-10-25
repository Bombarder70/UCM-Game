using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class Movement : MonoBehaviour  { 

	public PlayerController.Keyboard xxx; 

	void Start() {  
		xxx = new PlayerController.Keyboard();
		xxx.getSettings();
	}  
  
	void Update() {  
		if (Input.GetKey(KeyCode.D)) {  
			transform.Translate(0.1f, 0f, 0f);  
		}  

		if (Input.GetKey(KeyCode.A)) {  
			transform.Translate(-0.1f, 0f, 0f);  
		}

		if (Input.GetKey(KeyCode.S)) {  
			transform.Translate(0.0f, 0f, -0.1f);  
		} 
 
		if (Input.GetKey(KeyCode.W)) {  
			transform.Translate(0.0f, 0f, 0.1f);  
		}  
	}  
} 