using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    public float moveSpeed = 5;
    public float maxSpeed = 10;
    private float curSpeed = 0;

    private Rigidbody2D rb;
    private Vector2 input;
    

    //Animation
    public Animator PlayerAnimator;
    public Animator FeetAnimator;
    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = this.GetComponentInChildren<Animator>();
        FeetAnimator = this.transform.GetChild(0).GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Animator
        curSpeed = rb.velocity.magnitude;



        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (curSpeed < maxSpeed)
        {
            rb.AddForce(input * moveSpeed);
        }
        if (Vector2.SqrMagnitude(input) <= 0.1f)
        {
            PlayerAnimator.SetBool("Speed", true);
            FeetAnimator.SetBool("Speed", true);
        }
        else
        {
            PlayerAnimator.SetBool("Speed", false);
            FeetAnimator.SetBool("Speed", false);
        }


    }
}
