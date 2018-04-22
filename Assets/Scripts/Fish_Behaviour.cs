using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Behaviour : MonoBehaviour {

	public int speed = 50;
	private Rigidbody2D r2d;
	public GameObject Coin;
	public int Value = 3;

	private GameObject Manager;


	// Function called when the fish is created
	void Start () {
		// Get the rigidbody component
		r2d = GetComponent<Rigidbody2D>();
		Manager = GameObject.FindGameObjectWithTag ("ManagerObject");

	}

	void Update(){
		// Add a vertical speed to the fish

		r2d.AddForce(new Vector3(0, speed,0));
	}


	// Function called when the object goes out of the screen
	void OnBecameInvisible() {
		// Destroy the enemy
		Destroy(gameObject);
	} 

	void OnTriggerEnter2D(Collider2D hook){
		if (hook.gameObject.name == "Hook") {
			
			GameObject CoinParticle = Instantiate(Coin, transform.position, transform.rotation);

			Manager.GetComponent<BuildingManagerScript> ().currentMoney += Value;
		
			Destroy (gameObject);
			Destroy(CoinParticle, 0.5f);
		}
	}
}
