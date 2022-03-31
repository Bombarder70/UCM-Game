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
    private bool healthStop;

    private int fallingCounter = 0;

    private bool readyForAttack = false;
    private bool readyForAttackRun = false;
    private int attackModeTime = 0;

    //private bool isRecovery = false;

    Rigidbody rb;

    public PlayerController.Keyboard playerControllerKeyboard; 

    private bool stopMoving = false;

    void Start() {  
      playerControllerKeyboard = new PlayerController.Keyboard();
      playerControllerKeyboard.getSettings();

      animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
    }
 
    void Update() {  
      //playerControllerKeyboard.checkEscapePress();

      // Sleduj ci hrac neumrel
      checkIfDied();
      checkForRecovery();

      this.isRunning = animator.GetBool("isRunning");
      this.isReverse = animator.GetBool("isReverse");

      /*
        PLAYER ATTACK
      */
      this.readyForAttack = this.getAnimationName("ReadyForAttack"); // Attack mode idle
      this.readyForAttackRun = this.getAnimationName("ReadyForAttackRun"); // Attack mode running

      // Ak hrac nie je v mode utoku idle ani v rune tak vyber mec
      if (!this.readyForAttack && !this.readyForAttackRun) {
        if (Input.GetMouseButtonDown(0)) {
          this.stopMoving = true;
          animator.SetBool("PullOutTheSword", true);
        } else {
          animator.SetBool("PullOutTheSword", false);
        }
      }

      // Ak je v mode attack moze sa hrac hybat
      if (this.readyForAttack) this.stopMoving = false; 

      // Ak je v mode attack rataj cas, po sekundach do modu IDLE
      if (this.readyForAttack || this.readyForAttackRun) this.attackModeTime += 1;
      if (this.attackModeTime > 500) {
        animator.SetBool("goToIdle", true);
        this.attackModeTime = 0;
      }
      if (this.readyForAttack && (this.getAnimationTime() > 3)) {
        animator.SetBool("readyForAttackIdle1", true);
      }

      // Ak je pripraveny na utok moze sekat
      if (this.readyForAttack || this.readyForAttackRun) {
        if (Input.GetMouseButtonDown(0)) {
          animator.SetBool("isAttacking", true);
        } else if (this.getAnimationName("Attack")) {
          animator.SetBool("isAttacking", false);
        }
      }

      if (this.getAnimationName("Attack")) {
        animator.SetBool("isAttacking", false);
      }

      /*
        PLAYER ATTACK END
      */

      if (this.stopMoving == false) {
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
      }

      // Pohyb a otacanie
      var velocity = Vector3.forward * Input.GetAxis("Vertical") * this.speed;
      if (
        !this.getAnimationName("Idle_jump") 
        && !isFalling 
        && !this.getAnimationName("Landing")
        && !this.getAnimationName("DeadLanding")
        && !this.getAnimationName("getup1")
      ) {
        transform.Translate(velocity * Time.deltaTime * this.playerSpeed);
      }

      if (
        !this.getAnimationName("Landing")
        && !this.getAnimationName("DeadLanding")
        && !this.getAnimationName("getup1")
      ) {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * this.turnSpeed);
      }
    
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

      if (this.getAnimationName("Idle") && (getAnimationTime() > 15)) {
        animator.SetBool("isIdle", true);
      } else {
        animator.SetBool("isIdle", false);
      }

      this.checkIfFalling();
    }  

    void FixedUpdate() {
    }

    void OnCollisionStay() {
      this.isGrounded = true;
      animator.SetBool("isGrounded", true);
    }

    void OnCollisionExit(){
      this.isGrounded = false;
      animator.SetBool("isGrounded", false);
    }

    // Custom methods
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

        //Rataj updaty
        this.fallingCounter += 1;

        checkLanding();
      } else {
        this.isFalling = false;
        animator.SetBool("isFalling", false);
        this.fallingCounter = 0;
      }
    }

    void checkLanding() {
      if (getAnimationName("Falling") && this.fallingCounter > 100) {
        setAsDead();
      }
    }

    void checkIfDied() {
      if (!this.healthStop && this.isDead && (deathType())) {
        HealthMonitor.HealthValue += -1;
        this.healthStop = true;
      }
    }

    // Sem pojdu variacie smrti
    bool deathType() {
      if (
        getAnimationName("DeadLanding")
        // && sem ina smrt
      ) {
        return true;
      } else {
        return false;
      }
    }

    void setAsDead() {
      this.isDead = true;
      animator.SetBool("isDead", true);
    }

    void checkForRecovery() {
      // Ak sa prehrava animacia DeadLanding tak cakaj 3 sekundy a daj recovery ak su este nejake srdiecka
      if (HealthMonitor.HealthValue > 0) {
        if (
          getAnimationName("DeadLanding")
          && getAnimationTime() > 3
        ) {
          setAsRecovery();
        } else {
          //this.isRecovery = false;
          animator.SetBool("isRecovery", false);
        }
      }
    }

    void setAsRecovery() {
      this.isDead = false;
      animator.SetBool("isDead", false);

      //this.isRecovery = true;
      animator.SetBool("isRecovery", true);

      this.healthStop = false;
    }

  }

}
