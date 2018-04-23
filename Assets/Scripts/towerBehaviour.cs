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
    public int health;
    public string towerType;
    [Range(0f, 10f)]
    public float spread;

    private GameObject[] targets;
    private GameObject target;
    private float nextFire = 0f;
    private float radius = 1f;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude/2.5f;
        changeScale();
    }

    public void changeScale()
    {
        float scale = range / radius;
        rend.material.SetFloat("_Scale", scale);
    }
    public void changeShaderState(bool setState,float onAlpha,float offAlpha)
    {        
        if (setState)
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
        changeShaderState(Camera.main.GetComponent<cameraControl>().mapOn, Camera.main.GetComponent<cameraControl>().onAlpha, Camera.main.GetComponent<cameraControl>().offAlpha);
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
                    if (towerType == "Type1")
                    {
                        GameObject bullet = (GameObject)Instantiate(bulletType, transform.position, transform.rotation);
                        bullet.GetComponent<Rigidbody2D>().AddForce(lookDirection * bulletSpeed);

                        Destroy(bullet, rangeTime);
                    }
                    if (towerType == "Type2")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            GameObject bullet = (GameObject)Instantiate(bulletType, transform.position, transform.rotation);
                            lookDirection = new Vector2(lookDirection.x + Random.Range(-(i * spread / 10f), (i * spread / 10f)), lookDirection.y + Random.Range(-(i * spread / 10f), (i * spread / 10f)));
                            bullet.GetComponent<Rigidbody2D>().AddForce(lookDirection * bulletSpeed);

                            Destroy(bullet, rangeTime);
                        }
                    }
                }
            }
        }
    }
}
