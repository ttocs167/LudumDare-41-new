using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDayNight : MonoBehaviour {
    private Renderer rend;
    public float dayAlpha=0;
    public float nightAlpha = 0.1;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	public void StartDay () {
        rend.material.SetFloat("_Alpha", dayAlpha);
    }
    public void StartNight()
    {
        rend.material.SetFloat("_Alpha", nightAlpha);
    }
}
