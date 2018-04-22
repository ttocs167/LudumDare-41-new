using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

	public GameObject Fish_Prefab;
	public float spawnTime = 2;
	public float startWait;
	public int fishCount;
	Renderer rd;
	float leftEdge;
	float rightEdge;


	// Use this for initialization
	void Awake() {
		rd = GetComponent<Renderer>();
		leftEdge = transform.position.x - rd.bounds.size.x / 2;
		rightEdge = transform.position.x + rd.bounds.size.x / 2;

	}

	IEnumerator addFish(){

		Debug.Log ("Starting fish");


		yield return new WaitForSeconds (startWait);
			for (int i = 0; i < fishCount; i++) {		
				Vector2 spawnPoint = new Vector2 (Random.Range (leftEdge, rightEdge), transform.position.y);
				GameObject Fish = Instantiate (Fish_Prefab, spawnPoint, Quaternion.identity);
				Fish.transform.parent = gameObject.transform;
				yield return new WaitForSeconds (spawnTime);
			}

	
		
	}

	public void Stop(){
		StopAllCoroutines ();

		Debug.Log ("Stopping fish");
	}
}
