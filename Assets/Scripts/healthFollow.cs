using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthFollow : MonoBehaviour {
    private Camera cam;    
    private Transform target;
    public Vector2 offset=new Vector2(0,0);
    private string text="";
    void Start()
    {
        cam = Camera.main;
        target = this.gameObject.transform;
    }   
    public void setText(string tx)
    {
        text = tx;
    }
    void OnGUI()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        int sy = Screen.height - (int)screenPos.y;
        GUI.Label(new Rect(screenPos.x+offset.x, sy+offset.y, 100, 20), "Hello World!");
    }

}
