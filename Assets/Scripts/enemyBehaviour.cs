using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour {
    public float maxSpeed;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public int health;
    public float attackRate;
    public float chanceToTargetPlayer;

    private GameObject target;
    private Rigidbody2D rb;
    private float nextAttack;

	// Use this for initialization
	void Start () {
        target = GetTarget();
	}

    GameObject GetTarget()
    {
        float randomTarget = Random.value;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Tower");

        if (randomTarget > chanceToTargetPlayer && targets.Length != 0)
        {
            
            float minDist = Mathf.Infinity;
            GameObject tMin = null;
            foreach (GameObject t in targets)
            {
                float dist = Vector2.Distance(t.transform.position, this.transform.position);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            target = tMin;

        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        rb = GetComponent<Rigidbody2D>();
        return target;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target != null)
        {
            Vector2 heading = (target.transform.position - this.transform.position);
            float distance = heading.magnitude;
            Vector2 moveDirection = heading / distance;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 270;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if (distance > attackRange)
            {
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(moveDirection * moveSpeed);
                }
            }
            else
            {
                if (Time.time > nextAttack)
                {
                    nextAttack = Time.time + attackRate;
                    if (target.tag == "Player")
                    {
                        target.transform.gameObject.GetComponent<playerBehaviour>().health -= 1;

                        if (target.transform.gameObject.GetComponent<playerBehaviour>().health <= 0)
                        {
                            Debug.Log("PLAYER DEAD");
                            // kill player go to game over state
                        }
                    }
                    if (target.tag == "Tower")
                    {
                        target.transform.gameObject.GetComponent<towerBehaviour>().health -= 1;

                        if (target.transform.gameObject.GetComponent<towerBehaviour>().health <= 0)
                        {
                            Debug.Log("TOWER DEAD");
                            Destroy(target, 0);
                        }
                    }
                }
            }
        }
        else
        {
            target = GetTarget();
        }
    }
}
