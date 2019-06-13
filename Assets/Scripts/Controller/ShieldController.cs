using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;
    public bool isShieldOn;
    public float shieldCooldown;

    private float HEALTH_MAX;
    private float SHIELD_COOLDOWN_MAX;

    void Start()
    {
        HEALTH_MAX = health;
        SHIELD_COOLDOWN_MAX = shieldCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            shieldCooldown -= Time.deltaTime;
        }

        if (shieldCooldown <= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.03f;
            gameObject.transform.localScale = new Vector3(25, 25, 1);
            health = HEALTH_MAX;
            shieldCooldown = SHIELD_COOLDOWN_MAX;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletScript TargetStatus = Bullet.GetComponent<BulletScript>();

        if (collision.gameObject.tag == "EnemyBullet")
        {
            health -= TargetStatus.iBulletDamage;
        }
    }
}
