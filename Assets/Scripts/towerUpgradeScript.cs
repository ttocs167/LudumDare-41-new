using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerUpgradeScript : MonoBehaviour {

    public string towerType;
    public int price;
    private GameObject player;
    private GameObject manager;
    private GameObject[] towers;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("ManagerObject");
        towers = GameObject.FindGameObjectsWithTag(towerType);
    }
	

    public void OnButtonPress()
    {
        if (manager.transform.gameObject.GetComponent<phaseManager>().currentState == "BUILD" & manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney >= price)
        {
            manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney -= price;
            if (towerType == "BlueTower")
            {
                for (int i = 0; i < towers.Length; i++)
                {
                    towers[i].transform.gameObject.GetComponent<towerBehaviour>().rateOfFire += 0.2f;
                    towers[i].transform.gameObject.GetComponent<towerBehaviour>().range += 1f;

                }
            }
            else if (towerType == "SquidTower")
            {
                for (int i = 0; i < towers.Length; i++)
                {
                    towers[i].transform.gameObject.GetComponent<towerBehaviour>().rateOfFire += 0.2f;
                    towers[i].transform.gameObject.GetComponent<towerBehaviour>().range += 1f;
                    towers[i].transform.gameObject.GetComponent<towerBehaviour>().shotCount += 1;

                }
            }
        }
    }

}
