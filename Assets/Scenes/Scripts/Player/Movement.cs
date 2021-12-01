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
    public bool isGrounded;
    public bool isRunning;
    public bool isReverse;
    public bool isFalling;
    public bool isDead;

    Rigidbody rb;

    public PlayerController.Keyboard playerControllerKeyboard; 

    void Start() {  
      playerControllerKeyboard = new PlayerController.Keyboard();
      playerControllerKeyboard.getSettings();

      animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
    }
    
    void Update() {  
      //playerControllerKeyboard.checkEscapePress();

      this.isRunning = animator.GetBool("isRunning");
      this.isReverse = animator.GetBool("isReverse");

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
      if (
        !this.getAnimationName("Idle_jump") 
        && !isFalling 
        && !this.getAnimationName("Landing")
      ) {
        transform.Translate(velocity * Time.deltaTime * this.playerSpeed);
      }
      transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * this.turnSpeed);
      animator.SetFloat("Speed", velocity.z);

      if (isGrounded 
        && !isRunning
        && this.getAnimationName("Idle")
      ) {
        if (Input.GetButtonDown("Jump")) {
          animator.SetBool("isJumping", true);
          rb.AddForce(new Vector3(0, 80, 0), ForceMode.Impulse);
        }
      } else {
        animator.SetBool("isJumping", false);
      }

      if (Input.GetKeyDown("space")) {
        if (isRunning && isGrounded) {
          //animator.SetBool("isJumping", true);
          Debug.Log("run_jump");
          rb.AddForce(new Vector3(0, 85, 0), ForceMode.Impulse);
        }
      }

      if (!isRunning) {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 10) {
          animator.SetBool("isHappy", true);
        }
      }

      this.checkIfFalling();

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
       //if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !Animator.IsInTransition(0))
    }  

    void FixedUpdate() {
      this.setIsHappyToFalse();
    }

    void OnCollisionStay() {
      this.isGrounded = true;
      animator.SetBool("isGrounded", true);
    }

    void OnCollisionExit(){
      this.isGrounded = false;
      animator.SetBool("isGrounded", false);
    }

    // Custome methods

    void setIsHappyToFalse() {
      animator.SetBool("isHappy", false);
    }

    bool getIsHappy() {
      return animator.GetBool("isHappy");
    }

    double getAnimationTime() {
      return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool getAnimationName(string name) {
      return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    void checkIfFalling() {
      if (!this.isGrounded && rb.velocity.y < -5) {
        this.isFalling = true;
        animator.SetBool("isFalling", true);
        //Ked pada viac ako 2 sekundy prepni
        if (getAnimationTime() > 4) {
          this.isDead = true;
          animator.SetBool("isDead", true);
        }
      } else {
        this.isFalling = false;
        animator.SetBool("isFalling", false);
      }
    }

  }

}
