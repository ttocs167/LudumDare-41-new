﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTowerBehaviourScript : MonoBehaviour {
    
    public float gridSize = 1;
    public GameObject towerType;
    public float spawnTime;
    public GameObject player;
    public List<int> locationListx;
    public List<int> locationListy;
    private GameObject self;
    private float distance;
    private float distancex;
    private float distancey;
    private float theta;
    private int posx;
    private int posy;
    private bool overlap;


	// Use this for initialization
	void Start () {
        self = this.gameObject;
        spawnTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        locationListx.Add(1);
        locationListx.Add(1);
        locationListy.Add(1);
        locationListy.Add(-1);


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


        if (distance > gridSize * 2)
        {
            cursorPos.x = playerPos.x + 2 * gridSize * Mathf.Cos(theta) ;
            cursorPos.y = playerPos.y + 2 * gridSize * Mathf.Sin(theta) ;
        }
        cursorPos.x = Mathf.Floor(cursorPos.x / gridSize) * (gridSize);
        cursorPos.y = Mathf.Floor(cursorPos.y / gridSize) * (gridSize);
        cursorPos.z = playerPos.z;
        transform.position = cursorPos;

        posx = (int)cursorPos.x;
        posy = (int)cursorPos.y;

        for (int i = 0; i < locationListy.Count; i++)
        {
            if (posx == locationListx[i] & posy == locationListy[i])
            {
                overlap = true;
            }
        }

        if (Input.GetMouseButtonDown(0) & Time.time > spawnTime + 0.2f & !overlap)
        {
            GameObject tower = (GameObject)Instantiate(towerType, transform.position, transform.rotation);
            locationListx.Add(posx);
            locationListy.Add(posy);
            Debug.Log("mouse clicked: meant to place tower");
            spawnTime = Time.time;
            overlap = false;
            Destroy(self);
            
            
        }

    }


}


