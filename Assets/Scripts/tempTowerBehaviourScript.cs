using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTowerBehaviourScript : MonoBehaviour {
    
    public float gridSize = 1;
    public GameObject towerType;
    public float spawnTime;
    private GameObject player;
    private GameObject self;
    private float distance;
    private float distancex;
    private float distancey;
    private float theta;
    private int posx;
    private int posy;
    private bool overlap;
    private GameObject manager;
    public int price;



    // Use this for initialization
    void Start () {
        self = this.gameObject;
        spawnTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("ManagerObject");



    }

    // Update is called once per frame
    void Update()
    {
        if (manager.transform.gameObject.GetComponent<phaseManager>().currentState != "BUILD" || Input.GetMouseButtonDown(1) )
        {
            Destroy(self);
        }


        overlap = false;
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var playerPos = player.transform.position;
        cursorPos.z = 1;
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
            cursorPos.x = playerPos.x + 2 * gridSize * Mathf.Cos(theta);
            cursorPos.y = playerPos.y + 2 * gridSize * Mathf.Sin(theta);
        }
        cursorPos.x = Mathf.Floor(cursorPos.x / gridSize) * (gridSize) + 0.5f * gridSize;
        cursorPos.y = Mathf.Floor(cursorPos.y / gridSize) * (gridSize) + 0.5f * gridSize;
        cursorPos.z = 1;
        transform.position = cursorPos;

        posx = (int)cursorPos.x;
        posy = (int)cursorPos.y;

        for (int i = 0; i < manager.transform.gameObject.GetComponent<BuildingManagerScript>().locationListx.Count; i++)
        {
            if (posx == manager.transform.gameObject.GetComponent<BuildingManagerScript>().locationListx[i] & posy == manager.transform.gameObject.GetComponent<BuildingManagerScript>().locationListy[i])
            {
                overlap = true;
            }
        }

        if (Input.GetMouseButtonDown(0) & Time.time > spawnTime + 0.2f & !overlap & manager.transform.gameObject.GetComponent<phaseManager>().currentState == "BUILD")
        {
            GameObject tower = (GameObject)Instantiate(towerType, transform.position, transform.rotation);
            manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney -= price;
            manager.transform.gameObject.GetComponent<BuildingManagerScript>().locationListx.Add(posx);
            manager.transform.gameObject.GetComponent<BuildingManagerScript>().locationListy.Add(posy);
            //Debug.Log("mouse clicked: meant to place tower");
            spawnTime = Time.time;

            overlap = false;
            Destroy(self);


        }


    }


}


