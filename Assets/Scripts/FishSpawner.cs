using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

	public GameObject Fish_Prefab;
	public float spawnTime = 2;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("addEnemy", 0, spawnTime);
	}

	void addEnemy(){
		Renderer rd = GetComponent<Renderer>();

		float leftEdge = transform.position.x - rd.bounds.size.x / 2;
		float rightEdge = transform.position.x + rd.bounds.size.x / 2;

		Vector2 spawnPoint = new Vector2 (Random.Range(leftEdge, rightEdge), transform.position.y);

		GameObject Fish = Instantiate (Fish_Prefab, spawnPoint, Quaternion.identity);
		Fish.transform.parent = gameObject.transform;
		
	}

}
