using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    public string bulletType = "standard";
    //standard, missile, missilelockson, missileheateffect

    //Universal
    public int damage = 1;
    public float speed = 1.0f;

    //Missile 
    public float acceleration = 0.1f;
    public float maxSpeed = 20.0f;
    public float delay = 0;
    public float lockOnDelay = 0;
    public string targetTag;
    public float targetDistanceNeeded = 1.0f;
    private float targetDistance;

   
    public bool explodeOnTargetLocation = false;
    public bool destroyOnImpact = true;

    private bool missileFired = false;
    private Vector3 targetLocation;

    public Rigidbody2D hitEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bulletType == "standard")
        {
            StartCoroutine("shootStandard");
        }
        else if ((bulletType == "missile") && (!missileFired))
        {
            StartCoroutine("shootMissile");
        }
        else if ((bulletType == "missilelockson") && (!missileFired))
        {
            if (string.IsNullOrEmpty(targetTag))
            {
                Debug.Log("Bullet is a missle that locks on but target is empty");
            }
            else
            {
                StartCoroutine("shootMissileLock");
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 1);
    }

    void shootStandard()
    {
        //var realbullet = Instantiate(bullet, transform.position, transform.rotation);
        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    void shootMissile()
    {
        speed += acceleration;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        transform.Translate(Vector2.up * speed * Time.deltaTime);   
    }

    void shootMissileLock()
    {
        while (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        if (delay < 0)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag(targetTag);
            GameObject closest = null;

            foreach (GameObject enemy in enemys)
            {
                Vector3 diff = enemy.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < targetDistanceNeeded)
                {
                    closest = enemy;
                    targetDistance = curDistance;
                }
            }

            if ((closest) && (!missileFired))
            {
                targetLocation = closest.transform.position;
                speed += acceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                if (explodeOnTargetLocation)
                {
                    //Stay on direction, presumable to explode
                    transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
                }
                else
                {
                    //Continue direction
                    //get
                    var direction = targetLocation - transform.position;
                    direction.Normalize();
                    Debug.Log(direction);
                    transform.Translate(direction * speed * Time.deltaTime);
                }
            }
            else
            {
                //No closest enemy then contiue.
                speed += acceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            if (missileFired)
            {
                speed += acceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnImpact)
        {

            if (gameObject.tag == "Friendly")
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    Destroy(gameObject);
                }
            }

            if (gameObject.tag == "Enemy")
            {
                if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "Shield"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnImpact)
        {

            if (gameObject.tag == "Friendly")
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    Destroy(gameObject);
                }
            }

            if (gameObject.tag == "Enemy")
            {
                if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "Shield"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
