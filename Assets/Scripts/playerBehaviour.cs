using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    public float moveSpeed = 5;
    public float maxSpeed = 10;

    private Rigidbody2D rb;
    private Vector2 input;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(input * moveSpeed);
        }
    }
}
