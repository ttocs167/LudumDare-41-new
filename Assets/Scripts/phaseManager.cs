using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class phaseManager : MonoBehaviour {
    private bool buildPhase = true;
    private bool fightPhase = false;
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

    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        timer.text = ("" + maxBuildTime);

        if (fightPhase && enemies.Length == 0)
        {
            Debug.Log("fight phase");
            for (int i = 0; i < enemyCount; i++) {
                Invoke("SpawnEnemies", 0f);
            }

            if (enemies.Length == 0)
            {
                buildPhase = !buildPhase;
                fightPhase = !fightPhase;
            }
        }
        if (buildPhase)
        {
            Debug.Log("build phase");
            buildTime -= Time.deltaTime;
            timer.text = "Time Left:" + Mathf.Round(buildTime);
            if (buildTime < 0)
            {
                timer.text = "FIGHT!";
                buildPhase = !buildPhase;
                fightPhase = !fightPhase;
                enemyCount += 1;
            }
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
