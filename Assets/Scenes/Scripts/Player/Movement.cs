using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

	public class Movement : MonoBehaviour  { 

    private float speed = 2.2f;
    private float turnSpeed = 150f;
    private float playerSpeed = 2f;
    private Animator animator;

    // PLAYER JUMP
    private float jump = 1.5f;
    private bool isGrounded;

    Rigidbody rb;

    public PlayerController.Keyboard playerControllerKeyboard; 

    void Start() {  
      playerControllerKeyboard = new PlayerController.Keyboard();
      playerControllerKeyboard.getSettings();

      animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay() {
      isGrounded = true;
    }

    void OnCollisionExit(){
      isGrounded = false;
    }
    
    void Update() {  
      //playerControllerKeyboard.checkEscapePress();

      var isRunning = animator.GetBool("isRunning");
      var isReverse = animator.GetBool("isReverse");

      if (Input.GetKey("w")) {
        animator.SetBool("isRunning", true);
      } else {
        animator.SetBool("isRunning", false);
      }

      if (Input.GetKey("s")) {
        animator.SetBool("isReverse", true);
        this.speed = 1.5f;
      } else {
        animator.SetBool("isReverse", false);
        this.speed = 2.2f;
      }

      // Pohyb a otacanie
      var velocity = Vector3.forward * Input.GetAxis("Vertical") * this.speed;
      transform.Translate(velocity * Time.deltaTime * this.playerSpeed);
      transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * this.turnSpeed);
      animator.SetFloat("Speed", velocity.z);

      if (isGrounded && !isRunning) {
        if (Input.GetButtonDown("Jump")) {
          animator.SetBool("isJumping", true);
          rb.AddForce(new Vector3(0, 80, 0), ForceMode.Impulse);
        }
      } else {
        animator.SetBool("isJumping", false);
      }

      if (Input.GetKeyDown("space")) {
        if (isRunning && isGrounded) {
          Debug.Log("run_jump");
            rb.AddForce(new Vector3(30, 120, 0), ForceMode.Impulse);
        }
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

      // Poznamky

      // Moze sa vyuzit pri objektoch ktore hned vyletia 
      // transform.Translate(0,this.jump*Input.GetAxis("Jump")*Time.deltaTime,0);

      // Vypise sa iba raz ked sa stlaci
      // Input.GetKeyDown("w")
    }  
  }

}
