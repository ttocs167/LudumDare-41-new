using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingRain : MonoBehaviour {
    public float scrollingSpeed;
    private Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update () {        
        float offset = rend.material.GetFloat("_Offset"); ;         
        offset += Time.deltaTime * scrollingSpeed;
        rend.material.SetFloat("_Offset", offset);        
	}
}
