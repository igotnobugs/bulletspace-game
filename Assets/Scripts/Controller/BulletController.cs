using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 2);
    }


    void OnCollisionEnter2D(Collision2D collision)
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
