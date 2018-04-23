using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour {
    public GameObject bloodHit;
    public GameObject bloodDeath;
    public int damage;

    private GameObject bullet;
    private GameObject manager;

	// Use this for initialization
	void Start () {
        bullet = this.gameObject;
        manager = GameObject.FindGameObjectWithTag("ManagerObject");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(bullet.GetComponent<CircleCollider2D>()); // This line is to prevent multiple hits from one bullet

            GameObject HitParticle = (GameObject)Instantiate(bloodHit, transform.position, transform.rotation);

            collision.transform.gameObject.GetComponent<enemyBehaviour>().health -= damage;

            int healthCheck = collision.transform.gameObject.GetComponent<enemyBehaviour>().health;

            if (healthCheck <= 0)
            {
                Destroy(collision.transform.gameObject);
                GameObject deathParticle = (GameObject)Instantiate(bloodDeath, transform.position, transform.rotation);
                Destroy(deathParticle, 0.1f);

                manager.transform.gameObject.GetComponent<BuildingManagerScript>().currentMoney += Random.Range(1, 3);

            }

            Destroy(HitParticle, 0.1f);
            Destroy(bullet);
        }
        else
        {
            Destroy(bullet);
        }
    }
}
