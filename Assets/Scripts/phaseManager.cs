using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class phaseManager : MonoBehaviour {
    private bool buildPhase;
    private bool fightPhase;
    private int enemyCount = 1;
    private float buildTime;
    private GameObject[] enemies;

    public float width = 1f;
    public float height = 1f;
    public GameObject spawnArea;
    public GameObject enemyType;
    public float maxBuildTime;
    public Text timer;

    // Use this for initialization
    void Start()
    {
        buildPhase = true;
        fightPhase = false;
        buildTime = maxBuildTime;
        timer.text = ("" + maxBuildTime);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (buildPhase)
        {
            buildTime -= Time.deltaTime;
            timer.text = ("" + buildTime);
            // do build mode things

            if (buildTime < 0)
            {
                
                buildPhase = !buildPhase;
                fightPhase = !fightPhase;
            }
        }
        if (fightPhase)
        {
            fightPhase = !fightPhase;
            if (enemies.Length == 0)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    Invoke("SpawnEnemies", 0f);
                }
            }
        }
        if (enemies.Length == 0 && buildPhase == false && fightPhase == false)
        {
            buildPhase = !buildPhase;
            buildTime = maxBuildTime;
            enemyCount += 1;
        }
        
    }

    void SpawnEnemies()
    {
        float xPos = Random.Range(spawnArea.transform.position.x - width, spawnArea.transform.position.x + width);
        float yPos = Random.Range(spawnArea.transform.position.y - height, spawnArea.transform.position.y + height);
        Vector2 spawnLocation = new Vector2(xPos, yPos);
        GameObject enemy = (GameObject)Instantiate(enemyType, spawnLocation, transform.rotation);


    }
}
