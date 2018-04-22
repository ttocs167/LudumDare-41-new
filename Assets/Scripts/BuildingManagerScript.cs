using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerScript : MonoBehaviour {
    public List<int> locationListx;
    public List<int> locationListy;

    // Use this for initialization
    void Start () {
        locationListx.Add(1);
        locationListx.Add(1);
        locationListy.Add(1);
        locationListy.Add(-1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
