using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    public float moveSpeed = 5;
    public float maxSpeed = 10;
    public int health;
    private float curSpeed = 0;

    private Rigidbody2D rb;
    private Vector2 input;

	public GameObject Hook_prefab;
	private bool fishing = false;
	//Hook variables for control
	public GameObject FishSpawner;
	public float HookSpeed = 5.0f;
	private Vector3 hookInput;
	private Renderer FishSpawnRd;
	float leftEdge;
	float rightEdge;

	private GameObject Rod;
	private AudioSource HookReel;

    //Animation
    public Animator PlayerAnimator;
    public Animator FeetAnimator;
    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = this.GetComponentInChildren<Animator>();
        FeetAnimator = GameObject.FindWithTag("PlayerFeet").GetComponent<Animator> ();
		fishing = false;
		Rod = this.transform.GetChild (1).gameObject;
		HookReel = Hook_prefab.GetComponent<AudioSource> ();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		//Animator
		curSpeed = rb.velocity.magnitude;

		if (!fishing) {
			MovePlayer ();
		}

		if (fishing) {
			
			MoveHook ();

		}
			

    }

	void MoveHook(){
		FishSpawnRd = FishSpawner.GetComponent<Renderer>();
		leftEdge = FishSpawner.transform.position.x - FishSpawnRd.bounds.size.x / 2;
		rightEdge = FishSpawner.transform.position.x + FishSpawnRd.bounds.size.x / 2;
		hookInput = new Vector3(Input.GetAxis("Horizontal"),0,0);
		Hook_prefab.transform.position += hookInput * HookSpeed * Time.deltaTime;


		if (Hook_prefab.transform.position.x <= leftEdge) {
			Hook_prefab.transform.position = new Vector2 (leftEdge, Hook_prefab.transform.position.y);
		} else if(transform.position.x >= rightEdge) {			
			Hook_prefab.transform.position = new Vector2 (rightEdge, Hook_prefab.transform.position.y);			
		}
	}

	void MovePlayer(){
		
		input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (curSpeed < maxSpeed) {
			rb.AddForce (input * moveSpeed);
		}
		if (Vector2.SqrMagnitude (input) >= 0.1f) {
			PlayerAnimator.SetBool ("Start", true);
			FeetAnimator.SetBool ("Start", true);
		} else {
			PlayerAnimator.SetBool ("Start", false);
			FeetAnimator.SetBool ("Start", false);
		}
	}

	void OnTriggerStay2D(Collider2D Beach){
		if (Beach.gameObject.name == "Beach") {
			if (Input.GetButtonDown("Jump")){
				fishing = !fishing;

				if (fishing) {
					Hook_prefab.SetActive(true);
					Hook_prefab.transform.position = new Vector3(transform.position.x +1, transform.position.y  ,0);

					transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
					Rod.SetActive (true);
					HookReel.Play ();
				
					//Debug.Log (transform.rotation);
				}
				if (!fishing) {
					Hook_prefab.SetActive(false);
					Rod.SetActive (false);
					HookReel.Stop ();
				}
				//Debug.Log (fishing);

			}

		}

	}

}