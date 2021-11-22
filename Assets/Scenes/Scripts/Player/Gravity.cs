using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

  private float _gravity = 3f;
  private float _playerDistanceToGround = 0.5f;
  private Rigidbody _piratBody;

  private Animator animator;
  private bool isJumping = false;

  void Start() {
    _piratBody = GetComponent<Rigidbody>();
    _piratBody.useGravity = true;

    //nimator = GetComponent<Animator>();
  }

  void Update() {
    isGrounded();

    Debug.Log(_piratBody.velocity);
    _piratBody.AddForce(Physics.gravity * (_piratBody.velocity.y *_gravity), ForceMode.Acceleration);
  }

  void isGrounded() {
    var playerOnGround = Physics.Raycast(transform.position, -Vector3.up, _playerDistanceToGround + 0.1f);

    if (playerOnGround == false && isJumping == false) {
      //_piratBody.AddForce(Physics.gravity * (6 *_gravity), ForceMode.Acceleration);
    }
  }

}
