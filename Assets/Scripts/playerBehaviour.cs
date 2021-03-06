using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    public float moveSpeed = 5;
    public float maxSpeed = 10;
    public int health;
    public int maxHealth;
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
    private RobertHealthFollow RHF;
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
        health = maxHealth;
        RHFOnStart();
        SaySomething();
    }
	void RHFOnStart()
    {
        RHF = this.GetComponent<RobertHealthFollow>();
        if (RHF != null)
        {            
            RHFOnHealth();
        }        
    }
    void RHFOnHealth()
    {
        if (RHF != null)
        {
            RHF.setHealth(health, maxHealth);
        }       
    }    
    void SaySomething()
    {        
        if (RHF != null)
        {
            RHF.setText("");
        }
        if (Random.Range(0, 1f) > 0.9)
        {
            
            if (RHF != null)
            {
                if(Random.value<0.25)
                {
                    RHF.setText("Time To Get Some Fish!");
                }
                else if (Random.value < 0.5)
                {
                    RHF.setText("I Caught a Triangulon Once!");
                }
                else if (Random.value < 0.75)
                {
                    RHF.setText("Here Come's a Big One!");
                }
                else if (Random.value < 0.99)
                {
                    RHF.setText("Crunctime Was Better!");
                }
                else
                {
                    RHF.setText("My Wife Doesn't Love Me!");
                }
                
                
            }
        }
        Invoke("SaySomething", 2);
    }
	// Update is called once per frame
	void FixedUpdate () {
        RHFOnHealth();

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
            //Debug.Log("False");
            MovePlayer ();
		}

		if (fishing) {
            PlayerAnimator.SetBool("Fishing", true);
            rodAnimator.SetBool("Fishing", true);
            MoveHook();
            //Debug.Log("True");
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
            //Debug.Log(curSpeed);
		} else {
			PlayerAnimator.SetBool ("Start", false);
			FeetAnimator.SetBool ("Start", false);
		}
	}

    public void UpgradeHookSpeed()
    {
        Debug.Log("HOOK SPEED UPGRADED");
        // gold cost and upgrade values
    }

    public void UpgradeHookSize()
    {
        Debug.Log("HOOK SIZE UPGRADED");
        // gold cost and upgrade values
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