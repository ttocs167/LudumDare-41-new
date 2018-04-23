using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerUpgradeScript : MonoBehaviour {

    public string towerType;
    public int price;
    public Text textbox;
    private GameObject player;
    private GameObject manager;
    private GameObject[] towers;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("ManagerObject");
    }

    public void Update()
    {
        towers = GameObject.FindGameObjectsWithTag(towerType);
        if (towerType == "BlueTower")
        {
            price = 200 * towers.Length;
        }
        else if (towerType == "SquidTower")
        {
            price = 300 * towers.Length;
        }

        textbox.text = "" + price;
        Debug.Log("" + price);

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
