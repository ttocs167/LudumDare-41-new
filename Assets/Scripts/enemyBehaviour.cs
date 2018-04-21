using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour {
    public float maxSpeed;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public int health;

    private GameObject target;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 heading = (target.transform.position - this.transform.position);
        float distance = heading.magnitude;
        Vector2 moveDirection = heading / distance;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg +270;
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
            // attack
            //Debug.Log("ATTACK!");
        }
        

    }
}
