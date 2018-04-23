using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartEndSFXManager : MonoBehaviour {

	private GameObject phaseManager;

	AudioSource aSources;
	public AudioClip woo;
	public AudioClip success;
	public AudioClip monster;

	bool playedMonster = false;
	bool playedSucess = true;


	// Use this for initialization
	void Start () {
		phaseManager = GameObject.FindGameObjectWithTag ("ManagerObject");

		aSources = GetComponent<AudioSource>();

		}
	
	// Update is called once per frame
	void FixedUpdate () {

		if ((phaseManager.GetComponent<phaseManager> ().currentState == "BUILD") && (!playedSucess)) {
			aSources.PlayOneShot (woo, 0.7f);
			aSources.PlayOneShot (success, 0.7f);
			playedMonster = false;
			playedSucess = true;

		}

		if ((phaseManager.GetComponent<phaseManager> ().currentState == "SPAWN")  && (!playedMonster)) {
			aSources.PlayOneShot (monster, 0.7f);
			playedSucess = false;
			playedMonster = true;
		}
		
	}
}

