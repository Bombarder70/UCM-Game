using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
	private Animator animator;
	private Rigidbody rb;

	public bool isGrounded;
	public bool isRunning;

	void Start() {  
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}
    
	void Update() {
		
	}

	
}
