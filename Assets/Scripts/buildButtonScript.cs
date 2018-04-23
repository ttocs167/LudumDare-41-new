﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildButtonScript : MonoBehaviour
{

    public float gridSize = 1;
    public GameObject towerType;
    private GameObject player;
    private GameObject manager;
    public int price;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("ManagerObject");


    }

    // Update is called once per frame
    public void OnButtonPress()
    {
        if (manager.transform.gameObject.GetComponent<phaseManager>().currentState == "BUILD" & manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney >= price)
        {
            // Debug.Log("Button Clicked: meant to spawn tower");
            GameObject tower = (GameObject)Instantiate(towerType, player.transform.position, transform.rotation);
        }
    }


}
