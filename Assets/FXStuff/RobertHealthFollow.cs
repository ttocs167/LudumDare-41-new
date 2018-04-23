using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertHealthFollow : MonoBehaviour {
    private Camera cam;    
    private Transform target;
    public Vector2 offsetText=new Vector2(0,0);
    public Vector2 offset = new Vector2(0, 0);
    private string text="";
    private float maxHealth = 0;
    private float health = 0;
    public GUISkin gs;
    public Vector2 maxHealthSize = new Vector2(20, 5);
    public Vector2 healthSize = new Vector2(20,5);    
    void Start()
    {        
        cam = Camera.main;
        target = this.gameObject.transform;
        healthSize = maxHealthSize;        
    }   
    public void setText(string tx)
    {
        text = tx;
    }    
    public void setHealth(int h, int mH)
    {
        health = (float)h;
        maxHealth = (float)mH;
        if(maxHealth!=0)
        {
            healthSize = new Vector2(maxHealthSize.x * (float)(health / maxHealth), maxHealthSize.y);
        }        
    }

    void OnGUI()
    {
        if(Camera.main.GetComponent<cameraControl>().mapOn&&(Time.timeScale!=0))
        {
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);
            int sy = Screen.height - (int)screenPos.y;
            

            if (gs != null)
            {
                GUI.skin = gs;
                GUI.Label(new Rect(screenPos.x + offsetText.x, sy + offsetText.y, 100, 100), text);
                GUI.Box(new Rect(screenPos.x + offset.x, sy + offset.y, maxHealthSize.x, maxHealthSize.y), "");
                GUI.Box(new Rect(screenPos.x + offset.x, sy + offset.y, healthSize.x, healthSize.y), "", gs.customStyles[0]);
            }
            else
            {
                GUI.Label(new Rect(screenPos.x + offsetText.x, sy + offsetText.y, 100, 20), text);
                GUI.Box(new Rect(screenPos.x + offset.x, sy + offset.y, healthSize.x, healthSize.y), "");
            }
        }
        
    }

}
