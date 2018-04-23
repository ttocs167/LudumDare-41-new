using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManagerScript : MonoBehaviour {
    public List<int> locationListx;
    public List<int> locationListy;
    public int towerCount = 0;
    public int currentMoney;
    public Text Gold;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Gold.text = (""+currentMoney);
        


    }
}
