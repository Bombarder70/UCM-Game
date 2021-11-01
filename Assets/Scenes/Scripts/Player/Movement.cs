using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class Movement : MonoBehaviour  { 

	private float speed = 2f;
	private float turnSpeed = 150f;
	private Animator animator;

	public PlayerController.Keyboard xxx; 

	void Start() {  
		xxx = new PlayerController.Keyboard();
		xxx.getSettings();

		animator = GetComponent<Animator>();
	}  
  
	void Update() {  
		
		var velocity = Vector3.forward * Input.GetAxis("Vertical") * this.speed;
		transform.Translate(velocity * Time.deltaTime);
		transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * this.turnSpeed);
		animator.SetFloat("Speed", velocity.z);

		/*if (Input.GetKey(KeyCode.D)) {  
			transform.Translate(0.01f, 0f, 0f);  
		}  

		if (Input.GetKey(KeyCode.A)) {  
			transform.Translate(-0.01f, 0f, 0f);  
		}

		if (Input.GetKey(KeyCode.S)) {  
			transform.Translate(0.0f, 0f, -0.01f);  
		} 
 
		if (Input.GetKey(KeyCode.W)) {  
			transform.Translate(0.0f, 0f, 0.01f);  
		}*/
	}  
} 