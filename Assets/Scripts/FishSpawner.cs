using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

	public GameObject Fish_Prefab;
    public GameObject Fish2_Prefab;
    public GameObject Tri_Prefab;
    public GameObject Octo_Prefab;
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
				int fishSize = Random.Range (1, 30);
                
			if (fishSize < 5) {
                GameObject Fish = Instantiate(Fish_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = smFishVal;
				Fish.GetComponent<Fish_Behaviour>().speed = -smFishSpeed;
                Fish.transform.localScale = new Vector3 (smFishScale, smFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }	
			if (fishSize >= 6 && fishSize <= 8) {
                GameObject Fish = Instantiate(Fish_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = medFishVal;
				Fish.GetComponent<Fish_Behaviour>().speed = -medFishSpeed;
				Fish.transform.localScale = new Vector3 (medFishScale, medFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
			if (fishSize >= 9 && fishSize <= 12) {
                GameObject Fish = Instantiate(Fish_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = lgFishVal;
				Fish.GetComponent<Fish_Behaviour>().speed = -lgFishSpeed;
				Fish.transform.localScale = new Vector3 (lgFishScale, lgFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
            if (fishSize >= 13 && fishSize <= 16)
            {
                GameObject Fish = Instantiate(Fish2_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = medFishVal;
                Fish.GetComponent<Fish_Behaviour>().speed = -medFishSpeed;
                Fish.transform.localScale = new Vector3(medFishScale, medFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
            if (fishSize >= 17 && fishSize <= 21)
            {
                GameObject Fish = Instantiate(Fish2_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = lgFishVal;
                Fish.GetComponent<Fish_Behaviour>().speed = -lgFishSpeed;
                Fish.transform.localScale = new Vector3(lgFishScale, lgFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
            if (fishSize >= 21 && fishSize <= 24)
            {
                GameObject Fish = Instantiate(Octo_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = medFishVal;
                Fish.GetComponent<Fish_Behaviour>().speed = -medFishSpeed;
                Fish.transform.localScale = new Vector3(smFishScale, smFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
            if (fishSize >= 25 && fishSize <= 28)
            {
                GameObject Fish = Instantiate(Octo_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = lgFishVal;
                Fish.GetComponent<Fish_Behaviour>().speed = -lgFishSpeed;
                Fish.transform.localScale = new Vector3(medFishScale, medFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
            if (fishSize == 29)
            {
                GameObject Fish = Instantiate(Tri_Prefab, spawnPoint, Quaternion.identity);
                Fish.GetComponent<Fish_Behaviour>().Value = lgFishVal;
                Fish.GetComponent<Fish_Behaviour>().speed = -lgFishSpeed;
                Fish.transform.localScale = new Vector3(smFishScale, smFishScale, 1);
                Fish.transform.parent = gameObject.transform;
            }
            yield return new WaitForSeconds (spawnTime);
			}

	
		
	}

	public void Stop(){
		StopAllCoroutines ();

		//Debug.Log ("Stopping fish");
	}
}
