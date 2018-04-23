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

	private GameObject GameManager;
	private string GamePhase;

	private GameObject Rod;
	private AudioSource HookReel;

    //Animation
    private Animator PlayerAnimator;
    private Animator FeetAnimator;
    private Animator rodAnimator;
    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GameObject.FindWithTag("PlayerSprite").GetComponent<Animator>();
        FeetAnimator = GameObject.FindWithTag("PlayerFeet").GetComponent<Animator> ();
        rodAnimator = this.transform.GetChild (1).gameObject.GetComponent<Animator>();
        fishing = false;
		Rod = this.transform.GetChild (1).gameObject;
		HookReel = Hook_prefab.GetComponent<AudioSource> ();
		GameManager = GameObject.FindGameObjectWithTag ("ManagerObject");

    }
	
	// Update is called once per frame
	void FixedUpdate () {

		GamePhase = GameManager.GetComponent<phaseManager> ().currentState;


		//Animator
		curSpeed = rb.velocity.magnitude;

		if (GamePhase == "BUILD") {
			fishing = false;
			Hook_prefab.SetActive(false);
			Rod.SetActive (false);
			HookReel.Stop ();
		}


		if (!fishing) {
            PlayerAnimator.SetBool("Fishing", false);
            rodAnimator.SetBool("Fishing", false);
            Debug.Log("False");
            MovePlayer ();
		}

		if (fishing) {
            PlayerAnimator.SetBool("Fishing", true);
            rodAnimator.SetBool("Fishing", true);
            MoveHook();
            Debug.Log("True");
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
            if (rb.velocity.x < 0)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, Vector2.Angle(Vector2.up, rb.velocity / curSpeed));
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -Vector2.Angle(Vector2.up, rb.velocity / curSpeed));
            }
            Debug.Log(curSpeed);
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