using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaudeEffects : MonoBehaviour {

	private AudioSource ClaudeFX;

	// Use this for initialization
	void Start () {
		ClaudeFX = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(){
		
		ClaudeFX.Play ();

	}
}
