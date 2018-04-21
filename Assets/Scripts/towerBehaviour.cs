using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBehaviour : MonoBehaviour
{
    public Renderer rend;
    private bool isShaderOn = false;

    public float range = 20;
    public float damage;
    public float rateOfFire;
    public GameObject bulletType;
    public float bulletSpeed;
    public float rangeTime = 2f;

    private GameObject[] targets;
    private GameObject target;
    private float nextFire = 0f;
    private float radius = 1f;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude/2;
        changeScale();
    }

    public void changeScale()
    {
        float scale = range / radius;
        rend.material.SetFloat("_Scale", scale);
    }
    public void changeShaderState(float onAlpha,float offAlpha)
    {
        isShaderOn = !isShaderOn;
        if (isShaderOn)
        {
            rend.material.SetFloat("_Alpha", onAlpha);
            rend.material.SetColor("_OutlineColour", Color.red);
        }
        else
        {
            rend.material.SetFloat("_Alpha", offAlpha);
            rend.material.SetColor("_OutlineColour", Color.white);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            changeShaderState(0, 0.3f);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
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

        if (target != null)
        {
            Vector2 heading = (target.transform.position - this.transform.position);
            var distance = heading.magnitude;
            Vector2 lookDirection = heading / distance;

            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (heading.sqrMagnitude < range * range)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + rateOfFire;

                    GameObject bullet = (GameObject)Instantiate(bulletType, transform.position, transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(lookDirection * bulletSpeed);

                    Destroy(bullet, rangeTime);
                }
            }
        }
    }
}
