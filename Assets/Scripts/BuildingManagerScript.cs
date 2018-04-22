using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManagerScript : MonoBehaviour {
    public List<int> locationListx;
    public List<int> locationListy;
    public int currentMoney;
    public Text Gold;
    
    private GameObject goldDisplay;

    // Use this for initialization
    void Start () {
        currentMoney = 450;
    }
	
	// Update is called once per frame
	void Update () {
        Gold.text = (""+currentMoney);
        


    }
}
