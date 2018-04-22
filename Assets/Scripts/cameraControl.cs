using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {
    public float offAlpha = 0;
    public float onAlpha = 0.3f;
    public bool mapOn = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            mapOn = !mapOn;
        }

    }
}
