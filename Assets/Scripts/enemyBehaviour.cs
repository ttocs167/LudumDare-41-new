using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyBehaviour : MonoBehaviour {
    public float maxSpeed;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public int health;
    private int maxHealth;
    public float attackRate;
    public float chanceToTargetPlayer;

    private GameObject target;
    private Rigidbody2D rb;
    private float nextAttack;
    private RobertHealthFollow RHF;

 

    // Use this for initialization
    void Start () {             
        target = GetTarget();        
        maxHealth = health;
        RHFOnStart();
        SaySomething();
    }

    void RHFOnStart()
    {
        RHF = this.GetComponent<RobertHealthFollow>();
        if (RHF != null)
        {
            RHFOnHealth();
        }
    }
    void RHFOnHealth()
    {
        if (RHF != null)
        {
            RHF.setHealth(health, maxHealth);
        }
    }
    void SaySomething()
    {
        if (RHF != null)
        {
            RHF.setText("");
        }
        if (Random.Range(0, 1f) > 0.9)
        {

            if (RHF != null)
            {
                RHF.setText("OMG IS THAT CLAUDE VAN CLAM!");
            }
        }
        Invoke("SaySomething", 2);
    }
    GameObject GetTarget()
    {        
        GameObject target1 = null;
        float randomTarget = Random.value;
        GameObject[] blueTargets = GameObject.FindGameObjectsWithTag("BlueTower");
        GameObject[] squidTargets =GameObject.FindGameObjectsWithTag("SquidTower");
        int turretNumbers = blueTargets.Length + squidTargets.Length;
        GameObject[] targets = null;
        if (turretNumbers != 0)
        {
            int choice = (int)(Random.value * turretNumbers);            
            if (choice < (blueTargets.Length))
            {
                targets = blueTargets;
            }
            else
            {
                targets = squidTargets;
            }
        }
                
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
            target1 = tMin;

        }
        else
        {
            target1 = GameObject.FindGameObjectWithTag("Player");
        }
        rb = GetComponent<Rigidbody2D>();        
        return target1;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RHFOnHealth();
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
                            SceneManager.LoadScene(1);
                        }
                    }
                    if ((target.tag == "BlueTower")|| (target.tag == "SquidTower") )
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
