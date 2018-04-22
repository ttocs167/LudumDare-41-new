using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDayNight : MonoBehaviour {
    private Renderer rend;
    public float dayAlpha=0;
    public float nightAlpha = 0.1f;
    public float nightTime = 1f;
    private float targetAlpha = 0;
    private float currentAlpha = 0;
    private float timer = 0f;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        
    }
	
	// Update is called once per frame
	public void StartDay(){
        targetAlpha = dayAlpha;
        //rend.material.SetFloat("_Alpha", dayAlpha);
    }
    public void StartNight()
    {
        targetAlpha = nightAlpha;
        //rend.material.SetFloat("_Alpha", nightAlpha);
    }

    void Update()
    {
        float delAlpha = Time.deltaTime * (nightAlpha - dayAlpha) / nightTime;
        if ((targetAlpha==dayAlpha))
        {
            if(currentAlpha>targetAlpha)
            {                
                currentAlpha -= delAlpha;
                rend.material.SetFloat("_Alpha", currentAlpha);
            }
            else if(currentAlpha < targetAlpha)
            {
                timer = 0f;
                currentAlpha = targetAlpha;
                rend.material.SetFloat("_Alpha", currentAlpha);
            }
        }
        else if ((targetAlpha == nightAlpha))
        {
            if (currentAlpha < targetAlpha)
            {
                currentAlpha += delAlpha;
                rend.material.SetFloat("_Alpha", currentAlpha);
            }
            else if (currentAlpha > targetAlpha)
            {
                timer = 0f;
                currentAlpha = targetAlpha;
                rend.material.SetFloat("_Alpha", currentAlpha);
            }
        }
    }
}
