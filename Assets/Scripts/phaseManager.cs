using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class phaseManager : MonoBehaviour {
    //string FIGHT/BUILD/NONE
    private string currentState;

    private int enemyCount = 1;
    private float buildTime;
    private GameObject[] enemiesArray;
    //List<GameObject> enemiesList = new List<GameObject>();

    public float width = 1f;
    public float height = 1f;
    public GameObject spawnArea;
    public GameObject enemyType;
    public float maxBuildTime;
    public Text timer;

	public bool FishSpawning;

	private GameObject FishSpawner;


    // Use this for initialization
    void Start()
    {
        currentState = "BUILD";
        buildTime = maxBuildTime;
        timer.text = ("" + maxBuildTime);
		FishSpawner = GameObject.Find("FishSpawner");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Current State = " + currentState);
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");

        switch (currentState)
        {
            case "BUILD":
                buildState();
                break;
            case "SPAWN":
                spawnState();
                break;
            case "FIGHT":
                fightState();
                break;
        }
    }

    void buildState()
    {
		FishSpawner.GetComponent<FishSpawner> ().Stop();
		FishSpawning = false;
		buildTime -= Time.deltaTime;
        timer.text = ("" + buildTime);
        // do build mode things

        if (buildTime < 0)
        {
            currentState = "SPAWN";
        }
    }

    void spawnState()
    {
        currentState = "FIGHT";

        if (enemiesArray.Length == 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Invoke("SpawnEnemies", 0f);
            }
        }
    }

    void fightState()
    {
		
		//Debug.Log("enemiesList count = " + enemiesArray.Length);
        if (enemiesArray.Length == 0)
        {
            currentState = "BUILD";
            buildTime = maxBuildTime;
            enemyCount += 1;
        }
    }

    void SpawnEnemies()
	{
		float xPos = Random.Range (spawnArea.transform.position.x - width, spawnArea.transform.position.x + width);
		float yPos = Random.Range (spawnArea.transform.position.y - height, spawnArea.transform.position.y + height);
		Vector2 spawnLocation = new Vector2 (xPos, yPos);
		GameObject enemy = (GameObject)Instantiate (enemyType, spawnLocation, transform.rotation);
		if (!FishSpawning){
			FishSpawner.GetComponent<FishSpawner> ().StartCoroutine ("addFish");
			FishSpawning = true;
		}
    }
		
}
