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

	public int smFishVal = 3;
	public int smFishSpeed = 50;
	public int smFishScale = 2;
	public int medFishVal = 7;
	public int medFishSpeed = 70;
	public int medFishScale = 4;
	public int lgFishVal = 10;
	public int lgFishSpeed = 100;
	public int lgFishScale = 6;

	// Use this for initialization
	void Awake() {
		rd = GetComponent<Renderer>();
		leftEdge = transform.position.x - rd.bounds.size.x / 2;
		rightEdge = transform.position.x + rd.bounds.size.x / 2;

	}

	IEnumerator addFish(){

		//Debug.Log ("Starting fish");


		yield return new WaitForSeconds (startWait);
			for (int i = 0; i < fishCount; i++) {		
				Vector2 spawnPoint = new Vector2 (Random.Range (leftEdge, rightEdge), transform.position.y);
				int fishSize = Random.Range (1, 3);						
				GameObject Fish = Instantiate (Fish_Prefab, spawnPoint, Quaternion.identity);
			if (fishSize == 1) {
				Fish.GetComponent<Fish_Behaviour>().Value = smFishVal;
				Fish.GetComponent<Fish_Behaviour>().speed = -smFishSpeed;
				Fish.transform.localScale = new Vector3 (smFishScale, smFishScale, 1);
			}	
			if (fishSize == 2) {
				Fish.GetComponent<Fish_Behaviour>().Value = medFishVal;
				Fish.GetComponent<Fish_Behaviour>().speed = -medFishSpeed;
				Fish.transform.localScale = new Vector3 (medFishScale, medFishScale, 1);
			}
			if (fishSize == 3) {
				Fish.GetComponent<Fish_Behaviour>().Value = lgFishVal;
				Fish.GetComponent<Fish_Behaviour>().speed = -lgFishSpeed;
				Fish.transform.localScale = new Vector3 (lgFishScale, lgFishScale, 1);
			}	
							
				
				Fish.transform.parent = gameObject.transform;
				yield return new WaitForSeconds (spawnTime);
			}

	
		
	}

	public void Stop(){
		StopAllCoroutines ();

		//Debug.Log ("Stopping fish");
	}
}
