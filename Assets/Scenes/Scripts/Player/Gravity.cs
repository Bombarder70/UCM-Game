using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
  public float gravity = 5f;

  Rigidbody rb;

  void Start() {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    rb.AddForce(Physics.gravity * gravity, ForceMode.Acceleration);
  }
}
