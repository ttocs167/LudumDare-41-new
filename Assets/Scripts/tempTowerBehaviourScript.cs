using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTowerBehaviourScript : MonoBehaviour {

    private GameObject self;
    public float gridSize = 1;
    public GameObject towerType;
    public float spawnTime;
    public GameObject player;
    private float distance;
    private float distancex;
    private float distancey;
    private float theta;


	// Use this for initialization
	void Start () {
        self = this.gameObject;
        spawnTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var playerPos = player.transform.position;
        cursorPos.z = 45;
        distancex = cursorPos.x - playerPos.x;
        distancey = cursorPos.y - playerPos.y;
        distance = Mathf.Sqrt(Mathf.Pow(distancex, 2) + Mathf.Pow(distancey, 2));
        theta = Mathf.Atan(distancey / distancex);
        if (distancex < 0)
        {
            theta += Mathf.PI;
        }
        Debug.Log(theta.ToString());

        if (distance > gridSize * 2)
        {
            cursorPos.x = playerPos.x + 2 * gridSize * Mathf.Cos(theta) ;
            cursorPos.y = playerPos.y + 2 * gridSize * Mathf.Sin(theta) ;
        }
        cursorPos.x = Mathf.Floor(cursorPos.x / gridSize) * (gridSize);
        cursorPos.y = Mathf.Floor(cursorPos.y / gridSize) * (gridSize);
        cursorPos.z = playerPos.z;
        transform.position = cursorPos;

        if (Input.GetMouseButtonDown(0) & Time.time > spawnTime + 0.2f)
        {
            GameObject tower = (GameObject)Instantiate(towerType, transform.position, transform.rotation);
            Debug.Log("mouse clicked: meant to place tower");
            Debug.Log((Time.time - spawnTime).ToString());
            spawnTime = Time.time;
            Destroy(self);
            
        }

    }


}


