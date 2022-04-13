using System.Collections;
using System.Collections.Generic;
using UnityEngine;



	public class Movement : MonoBehaviour  { 

    private float speed = 2.2f;
    private float turnSpeed = 150f;
    private float playerSpeed = 2f;
    private Animator animator;

    public static bool playerIsDead = false;

    // PLAYER JUMP
    public bool isGrounded;
    public bool isRunning;
    public bool isReverse;
    public bool isJumping;

    public bool isFalling;
    public bool isDead;

    private int fallingCounter = 0;

    private bool readyForAttack = false;
    private bool readyForAttackRun = false;
    private bool pullOutTheSword = false;
    public bool swordEquiped = false;

    private bool repeatAttack = false;
    private int clickCounter = 0;

    [SerializeField]
    Transform sword;
    public Transform sword_ueq_pos, sword_eq_pos;
    private float slowDown = 0f;

    private float transformX; 
    private float transformZ; 

    AnimatorClipInfo[] currentAnimationInfo;

    Rigidbody rb;

    public PlayerController.Keyboard playerControllerKeyboard; 

    private bool stopMoving = false;

    void Start() {  
      playerControllerKeyboard = new PlayerController.Keyboard();
      playerControllerKeyboard.getSettings();

      animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
    }

    public void die() {
      animator.SetBool("isDead", true);
      this.stopMoving = true;
    }

    void StopAttack() {
      // TODO: After Attack
    }
 
    void Update() {  
      //playerControllerKeyboard.checkEscapePress();

      // Sleduj ci hrac neumrel
      checkIfDied();
      checkForRecovery();

      this.isRunning = animator.GetBool("isRunning");
      this.isReverse = animator.GetBool("isReverse");
      this.isJumping = animator.GetBool("isJumping");

      /*
        PLAYER ATTACK
      */
      this.readyForAttack = this.getAnimationName("ReadyForAttack"); // Attack mode idle
      this.readyForAttackRun = this.getAnimationName("ReadyForAttackRun"); // Attack mode running
      this.pullOutTheSword = this.animatorIsPlaying("PullOutTheSword"); // PullOutTheSword
    
      /**
        * Pirat vytiahnutie mecu
        * Tlacidlo H
        * Ak hrac nie je v stave ReadyForAttack, ReadyForAttackRun a prave sa neprehrava animacia vythiahnutia
        */
      if (Input.GetKey("h")) { 
        if (!this.readyForAttack && !this.readyForAttackRun && !this.pullOutTheSword) {
          this.stopMoving = true; 
          animator.SetBool("PullOutTheSword", true);
        }
      } else animator.SetBool("PullOutTheSword", false);

      /**
        * Ak ma hrac vytiahnuty mec tak ho schovaj
       */
      if (this.readyForAttack || this.readyForAttackRun) {
        this.stopMoving = false; 

        if (Input.GetKey("h")) {
          animator.SetBool("goToIdle", true);
        }
      }

      if (swordEquiped)
      {
        sword.position = sword_eq_pos.position;
        sword.rotation = sword_eq_pos.rotation;
      }
      else
      {
        
        sword.position = sword_ueq_pos.position;
        sword.rotation = sword_ueq_pos.rotation;
      }


      // Mod idle animation
      if (this.readyForAttack && (this.getAnimationTime() > 3)) {
        animator.SetBool("readyForAttackIdle1", true);
      }
      // Ak zacne utekat alebo skakat prerus animaciu
      if (this.isRunning || this.isJumping || this.isReverse) animator.SetBool("readyForAttackIdle1", false);

      // Ak je pripraveny na utok moze sekat
      if (this.readyForAttack || this.readyForAttackRun) {
        this.slowDown = 0.5f;
        if (Input.GetMouseButtonDown(0)) {
          animator.SetBool("isAttacking", true);
        }
      } else {
        this.slowDown = 0;
      }
       
      if (this.getCurrentAnimationName() == "Sword And Shield Slash") {
        if (!this.repeatAttack) animator.SetBool("isAttacking", false);

        if (this.repeatAttack) this.clickCounter += 1;
        if (this.clickCounter > 50) {
          this.clickCounter = 0;
          this.repeatAttack = false;
        }

        if (Input.GetMouseButtonDown(0)) {
          if (this.clickCounter > 0) this.clickCounter = 0;
          this.repeatAttack = true;
        }
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
      var velocity = Vector3.forward * Input.GetAxis("Vertical") * (this.speed - this.slowDown);
      if (
        !this.getAnimationName("Idle_jump") 
        && !isFalling 
        && !this.getAnimationName("Landing")
        && !this.getAnimationName("DeadLanding")
        && !this.getAnimationName("getup1")
        && this.stopMoving == false
        && !this.getAnimationName("ReadyForAttackJump") 
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
        && (this.getAnimationName("Idle") || this.getAnimationName("ReadyForAttack"))
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
          rb.AddForce(new Vector3(0, 85, 0), ForceMode.Impulse);
          this.transformX = transform.position.x;
          this.transformZ = transform.position.z;
        }
      }

      /*if (isGrounded && this.getAnimationName("Run_jump") && (this.getAnimationTime() > 0.5)) {
        var x = (this.transformX - transform.position.x) * (-2);
        var z = (this.transformZ - transform.position.z) * (-2);
        rb.AddForce(new Vector3(x, 85, z), ForceMode.Impulse);
      }*/

      if (this.getAnimationName("Idle") && (getAnimationTime() > 15)) {
        animator.SetBool("isIdle", true);
      } else {
        animator.SetBool("isIdle", false);
      }

      this.checkIfFalling();
    }  

    void FixedUpdate() {
      if (animator.GetBool("goToIdle"))  animator.SetBool("goToIdle", false);
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
      if (getAnimationName("Falling") && this.fallingCounter > 65) {
        setAsDeadLanding();
      }
    }

    /**
      * Skontroluj ci je hrac mrtvy
      * Ak ma hrac 0 sridiecok tak nastav ako mrtvy
      * Ked sa prehrava animacia dead tak OFFICIALNE je hrac mrtvy
      * ak je hrac mrtvy vypne sa Movement controller
      */
    void checkIfDied() {
      if (Movement.playerIsDead == false) {
        if (HealthMonitor.HealthValue == 0) {
          if (this.animatorIsPlaying("dead")) Movement.playerIsDead = true;
          else this.die();
        }
      } else {
        animator.SetBool("isDead", false);
        gameObject.GetComponent<Movement>().enabled = false;
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

    void setAsDeadLanding() {
      animator.SetBool("isDeadLanding", true);
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
    }

    public void Sword_Equip() {
      swordEquiped = true;
    }

    public void Sword_Unequip() {
      swordEquiped = false;
    }

    bool animatorIsPlayingTime() {
		  return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	  }

    bool animatorIsPlaying(string animationName) {
      return animatorIsPlayingTime() && animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }	

    /**
      * Current Animation name
      * @return string
    */
    string getCurrentAnimationName() {
      currentAnimationInfo = this.animator.GetCurrentAnimatorClipInfo(0);
      return currentAnimationInfo[0].clip.name;
    }

  }



