using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool standard = true;
    public bool missile = false;
    public bool missileLocksOn = false;
    public bool missileHeatSeek = false;

    public int damage = 1;
    public float speed = 1.0f;
    public float acceleration = 0.2f;
    public float delay = 0;
    public float lockOnDelay = 0;
    public string targetTag;
    public bool explodeOnTargetLocation = false;

    public Rigidbody2D hitEffect;

    public bool destroyOnImpact = true;

    private bool missileFired = false;
    private Vector2 targetLocation;
    public bool enemyFound = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (standard)
        {
            StartCoroutine("shootStandard");
        }
        else if ((missile) && (!missileFired))
        {
            StartCoroutine("shootMissile");
        }
        else if ((missileLocksOn) && (!missileFired))
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
        Destroy(gameObject, 2);
    }

    void shootStandard()
    {
        //var realbullet = Instantiate(bullet, transform.position, transform.rotation);
        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    void shootMissile()
    {
        speed += acceleration;
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void shootMissileLock()
    {
        //new WaitForSeconds(delay);
        GameObject targetObject = GameObject.FindWithTag(targetTag);


        if ((targetObject) && (!missileFired))
        {
            targetLocation = targetObject.transform.position;

            if (explodeOnTargetLocation)
            {
                speed += acceleration;
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
            }
            else
            {
                //targetLocation.Normalize();
                speed += acceleration;
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
            }

        }
        else
        {
            //Nothing
            speed += acceleration;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        
        if (missileFired)
        {
            speed += acceleration;
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
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
