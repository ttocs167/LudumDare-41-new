using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour {
    public float width = 1f;
    public float height = 1f;
    public GameObject spawnArea;
    public GameObject enemyType;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Invoke("SpawnEnemies", 0f);
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
