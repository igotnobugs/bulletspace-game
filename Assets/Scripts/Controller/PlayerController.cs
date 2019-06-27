using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalVars;

public class PlayerController : MonoBehaviour
{
    //Requirements
    private STGFramework STGEngine;
    private AudioSource AudioSource;

    [HideInInspector]
    public Vector2 playerDirection;

    //The Ship
    private Rigidbody2D SelfRigidBody;

    //Ship Stats
    public int shipHealth = 3;
    public bool isAlive = true;
    public float fireRate = 50.0f; 
    public float shipSpeed = 5.0f;

    public bool isShieldDeployed = true;
    public int shieldHealth = 5;
    public float shieldDuration;
    public float shieldCooldown;

    public float heatSink;
    public float heatCoolRate;
    public float heatCooldown = 100;
    private bool heatIsCoolingDown = false;

    private float spin = 0.5f;

    [HideInInspector]
    public float FIRERATE_MAX, SHIELD_COOLDOWN_MAX, HEATSINK_MAX, HEAT_COOLDOWN_MAX, SHIP_HEALTH_MAX;

    //Ship Components
    public Rigidbody2D mainProjectile;
    public float mainHeatCost;
    public Rigidbody2D rCircleProjectile;
    public Rigidbody2D rShield;

    //Ship Audio
    public AudioClip BasicHitSound;
    public AudioClip DestroySound;

    //Player Controls
    // Default 3, 2.5, 11.5, 12.5.
    public bool bMoveable = true;

    public bool GodMode;
    private float fInviTime;
    private bool bHitOnce = false;

    public GunController theGun;
    public ShieldController theShield;
    public GameObject shield;

    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        AudioSource = GetComponent<AudioSource>();
        SelfRigidBody = GetComponent<Rigidbody2D>();

        FIRERATE_MAX = fireRate;
        SHIELD_COOLDOWN_MAX = shieldCooldown;
        HEATSINK_MAX = heatSink;
        HEAT_COOLDOWN_MAX = heatCooldown;
        SHIP_HEALTH_MAX = shipHealth;

        fireRate = 0;
        shieldCooldown = 0;
        heatSink = 0;
        heatCooldown = 0;
    }



    // Update is called once per frame
    void Update()
    {   
        if (heatSink > HEATSINK_MAX) {
            heatIsCoolingDown = true;
        }

        if (heatIsCoolingDown) {
            heatCooldown--;
        }

        if (heatCooldown <= 0) {
            heatIsCoolingDown = false;
            heatCooldown = HEAT_COOLDOWN_MAX;
        }

        if (heatSink > 0) {
            heatSink -= heatCoolRate;
        }

        if (fireRate > 0) {
            fireRate--;
        }

        if (isShieldDeployed) {
            shield.SetActive(true);
        }

        #region Controls Code
        PlayerShipMovement(GlobVars.kLeft, GlobVars.kRight, GlobVars.kUp, GlobVars.kDown, bMoveable);
        PlayerShipSpeedMovement(GlobVars.kBrake, shipSpeed);
        if ((Input.GetKey(GlobVars.kShoot)) && (isAlive))
        {
            theGun.isFiring = true;
        } else
        {
            theGun.isFiring = false;
        }
        //Suicide
        if (Input.GetKeyDown(GlobVars.kSuicide))
        {  
            shipHealth = 0;
        }
        #endregion


        if ((shipHealth <= 0) && (isAlive) && (!GodMode))
        {
            AudioSource.PlayOneShot(DestroySound, 1.0f);
            isAlive = false;
        }

        if (!isAlive)
        {
            spin += 5f;
            SelfRigidBody.bodyType = RigidbodyType2D.Dynamic;
            this.transform.rotation = Quaternion.Euler(180, 0, spin);
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            Destroy(gameObject, 1.0f);
        }

        //Invinsible time
        if (bHitOnce == true)
        {
            fInviTime--;
            if(fInviTime <= 0)
            {
                bHitOnce = false;
            }
        }

    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bullet = collision.gameObject.GetComponent<BulletController>();

        if ((collision.gameObject.tag == "EnemyBullet") && (!bHitOnce) && (shipHealth != 0))
        {
            shipHealth -= bullet.damage;
            AudioSource.PlayOneShot(BasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(bullet.hitEffect, this.transform.position, this.transform.rotation, new Vector2(0, 0), 0);
            fInviTime = 30;
            bHitOnce = true;
        }
    }

    #region Control Script
    void PlayerShipMovement(KeyCode LeftKey, KeyCode RightKey, KeyCode UpKey, KeyCode DownKey, bool bEnabled)
    {
        bool bReachedLimitLeft, bReachedLimitRight, bReachedLimitUp, bReachedLimitDown;
        //this.bPlayerControlEnabled = bEnabled;

        if (this.transform.position.x < GlobVars.BottomLeftBorder.x)
        { bReachedLimitLeft = true; }
        else { bReachedLimitLeft = false; }

        if (this.transform.position.x > GlobVars.UpperRightBorder.x)
        { bReachedLimitRight = true; }
        else { bReachedLimitRight = false; }

        if (this.transform.position.y > GlobVars.UpperRightBorder.y)
        { bReachedLimitUp = true; }
        else { bReachedLimitUp = false; }

        if (this.transform.position.y < GlobVars.BottomLeftBorder.y)
        { bReachedLimitDown = true; }
        else { bReachedLimitDown = false; }

        if (((!Input.GetKey(LeftKey)) && (!Input.GetKey(RightKey))) ||
            ((this.transform.position.x <= GlobVars.BottomLeftBorder.x) || (this.transform.position.x >= GlobVars.UpperRightBorder.x)))
        {
            playerDirection.x = 0;
        }

        if (((!Input.GetKey(DownKey)) && (!Input.GetKey(UpKey))) ||
            ((this.transform.position.y <= GlobVars.BottomLeftBorder.y) || (this.transform.position.y >= GlobVars.UpperRightBorder.y)))
        {
            playerDirection.y = 0;
        }

        if (bEnabled == true)
        {
            //Left 0 
            if (((Input.GetKeyDown(LeftKey)) && (playerDirection.x >= 0) ||
                (Input.GetKey(LeftKey)) && (Input.GetKeyUp(RightKey))) &&
                (!bReachedLimitLeft))
            {
                playerDirection.x = -1.0f;
            }

            //Right 1
            if (((Input.GetKeyDown(RightKey)) && (playerDirection.x <= 0) ||
                (Input.GetKey(RightKey)) && (Input.GetKeyUp(LeftKey))) &&
                (!bReachedLimitRight))
            {
                playerDirection.x = 1.0f;
            }

            //Down 2
            if (((Input.GetKeyDown(DownKey)) && (playerDirection.y >= 0) ||
                (Input.GetKey(DownKey)) && (Input.GetKeyUp(UpKey))) &&
                (!bReachedLimitDown))
            {
                playerDirection.y = -1.0f;
            }

            //Up 3
            if (((Input.GetKeyDown(UpKey)) && (playerDirection.y <= 0) ||
                (Input.GetKey(UpKey)) && (Input.GetKeyUp(DownKey))) &&
                (!bReachedLimitUp))
            {
                playerDirection.y = 1.0f;
            }
        }
    }

    void PlayerShipSpeedMovement(KeyCode kBrake, float speed, float divisorSpeed = 2.0f)
    {
        if (Input.GetKey(kBrake))
        {
            SelfRigidBody.velocity = playerDirection * speed / divisorSpeed;
        }
        else
        {
            SelfRigidBody.velocity = playerDirection * speed;
        }       
    }
    #endregion
}
