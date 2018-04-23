using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hookUpgradeScript : MonoBehaviour
{

    public string upgrade;
    public int price;
    public Text textbox;
    public GameObject hook;
    private GameObject player;
    private GameObject manager;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("ManagerObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.transform.gameObject.GetComponent<phaseManager>().currentState != "BUILD")
        {
            gameObject.GetComponent<Button>().interactable = false;

        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }

        textbox.text = "" + price;
    }

    public void OnButtonPress()
    {

        if (manager.transform.gameObject.GetComponent<phaseManager>().currentState == "BUILD" & manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney >= price)
        {
            manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney -= price;
            if (upgrade == "Size")
            {
                hook.transform.localScale += new Vector3(0.5f, 0.5f, 0f);
            }
            else if (upgrade == "Speed")
            {
                player.transform.gameObject.GetComponent<playerBehaviour>().HookSpeed += 1;
                
            }
        }
    }
}