using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldScript : MonoBehaviour {

    public float health;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        GameObject Player = GameObject.FindWithTag("Player");
        this.transform.position = Player.transform.position;     

        if (health <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Bullet = collision.gameObject;
        //BulletScript TargetStatus = Bullet.GetComponent<BulletScript>();

        if (collision.gameObject.tag == "EnemyBullet")
        {
            //health -= TargetStatus.iBulletDamage;
        }
    }
}
