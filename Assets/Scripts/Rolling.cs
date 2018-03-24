using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour {

	private Rigidbody rb;
	private int torque;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	

		target = PinSockets.pos [Random.Range(0, 10)];
		torque = 0;
	}

	private void FixedUpdate()
	{
		rb.AddForce ((target - transform.position).normalized * 100);
		rb.AddTorque (transform.up * torque);
	}
}
