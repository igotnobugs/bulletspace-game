using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private STGFramework STGEngine;
    private AudioSource AudioSource;

    //The Ship
    private Rigidbody2D SelfRigidBody;

    //Ship Stats
    public int shipHealth = 1;
    public bool isAlive = true;
    public float fireRate = 50.0f;
    public float shipSpeed = 1.0f;
    public float delayToFire = 2.0f;

    //Behavior
    public bool faceThePlayer = false;

    //public bool isShieldDeployed = false;
    //public float shieldHealth = 10.0f;
    //public float shieldDuration;
    //public float shieldCooldown;

    //Ship Audio
    public AudioClip BasicHitSound;
    public AudioClip DestroySound;
    public Rigidbody2D rFXHit;

    [HideInInspector]
    public float FIRERATE_MAX, SHIELD_COOLDOWN_MAX, HEATSINK_MAX, HEAT_COOLDOWN_MAX, SHIP_HEALTH_MAX;

    private float spin = 0.5f;

    public GunController theGun;
    //public ShieldController theShield;
    //public GameObject shield;

    // Start is called before the first frame update
    public bool debugDontFire;
    public bool debugInvulnerable;

    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        AudioSource = GetComponent<AudioSource>();
        SelfRigidBody = GetComponent<Rigidbody2D>();

        FIRERATE_MAX = fireRate;
        SHIP_HEALTH_MAX = shipHealth;

        fireRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * shipSpeed * Time.deltaTime);

        delayToFire -= Time.deltaTime;

        if (delayToFire <= 0) 
        {
            if ((theGun.isFiring == false) && (isAlive) && (!debugDontFire))
            {
                theGun.isFiring = true;
            }
        }

        if (faceThePlayer)
        {
            GameObject Target = GameObject.FindWithTag("Player");
            if (Target)
            {
                PlayerController targetscript = Target.GetComponent<PlayerController>();
                float targetspeed = targetscript.shipSpeed;
                Vector2 targetdirection = targetscript.playerDirection;
                float xpos = Target.transform.position.x + (targetscript.playerDirection.x * targetscript.shipSpeed) - this.transform.position.x;
                float ypos = Target.transform.position.y + (targetscript.playerDirection.y * targetscript.shipSpeed) - this.transform.position.y;
                float angle = Mathf.Atan2(ypos, xpos) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        if ((shipHealth <= 0) && (isAlive) && (!debugInvulnerable))
        {
            AudioSource.PlayOneShot(DestroySound, 1.0f);
            isAlive = false;
        }

        if (!isAlive)
        {
            spin += 5f;
            SelfRigidBody.bodyType = RigidbodyType2D.Dynamic;
            this.transform.rotation = Quaternion.Euler(180, 0, spin);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject, 1.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bullet = collision.gameObject.GetComponent<BulletController>();

        if ((collision.gameObject.tag == "Friendly"))
        {
            shipHealth -= bullet.damage;
            AudioSource.PlayOneShot(BasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(bullet.hitEffect,transform.position, transform.rotation, new Vector2(0, 0), 0);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 0);
    }
}
