using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class Movement : MonoBehaviour  { 

	private float speed = 2f;
	private float turnSpeed = 150f;
	private float playerSpeed = 2f;
	private Animator animator;

	// PLAYER JUMP
	private float jump = 100f;
	private bool isGrounded;

	public PlayerController.Keyboard xxx; 

	void Start() {  
		xxx = new PlayerController.Keyboard();
		xxx.getSettings();

		animator = GetComponent<Animator>();
	}

	void OnCollisionStay() {
		isGrounded = true;
	}

	void OnCollisionExit(){
		isGrounded = false;
	}
  
	void Update() {  
		
		var velocity = Vector3.forward * Input.GetAxis("Vertical") * this.speed;
		transform.Translate(velocity * Time.deltaTime * this.playerSpeed);
		transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * this.turnSpeed);
		animator.SetFloat("Speed", velocity.z);

		if(isGrounded) {
			transform.Translate(0,this.jump*Input.GetAxis("Jump")*Time.deltaTime,0);
		}

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