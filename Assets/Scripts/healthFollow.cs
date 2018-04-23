using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthFollow : MonoBehaviour {

    private Camera cam;    
    private Transform target;
    public Vector2 offset=new Vector2(0,0);
    public UnityEngine.UI.Slider slider;
    public GameObject character;
    private string text="";

    void Start()
    {
        cam = Camera.main;
        target = this.gameObject.transform;
        slider.maxValue = character.transform.gameObject.GetComponent<playerBehaviour>().maxHealth;


    }   
    public void Update()
    {
        //text = tx;
        slider.value = character.transform.gameObject.GetComponent<playerBehaviour>().health;
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        slider.transform.position = new Vector3 (screenPos.x + offset.x, screenPos.y + offset.y,-20);
    }
    //void OnGUI()
    //{

    //    // GUI.Label(new Rect(screenPos.x+offset.x, sy+offset.y, 100f, 20f), "Hello World!");
        
    //}

}
