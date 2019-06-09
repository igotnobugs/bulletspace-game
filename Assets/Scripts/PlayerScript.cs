using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //Needed to work
    private STGFramework STGEngine;
    private float fCooldownFireRate;
    private AudioSource aAudioSource;
    // Default 3, 2.5, 11.5, 12.5.
    public Vector2 vBotLeftCor;
    public Vector2 vUpRightCor;

    //Ship Components
    public Rigidbody2D rThisBody;
    public Rigidbody2D rProjectile;
    public Rigidbody2D rCircleProjectile;
    public Rigidbody2D rFXHit;
    public Rigidbody2D rShield;

    //Ship Audio
    public AudioClip aBasicShootSound;
    public AudioClip aBasicHitSound;
    public AudioClip aDestroySound;

    //Ship Stats
    public int iHealth;
    public bool bAlive = true;
    public float fOrigFireRate = 20;
    public float fSpeed = 1.0f;
    public float fShieldCooldown = 0;
    public float fShieldCooldownMax = 2000;
    private float spin = 0.5f;
    public int iBombAmmo = 2;
    private bool bBombDeployed = false;

    //Player Controls
    public KeyCode kShoot = KeyCode.Space;
    public KeyCode kLeft = KeyCode.A;
    public KeyCode kRight = KeyCode.D;
    public KeyCode kUp = KeyCode.W;
    public KeyCode kDown = KeyCode.S;
    public KeyCode kShield = KeyCode.E;
    public KeyCode kSuicide = KeyCode.F;
    public KeyCode kBrake = KeyCode.LeftShift;
    public KeyCode kQuit = KeyCode.Escape;
    public bool bMoveable = true;


    public bool GodMode;
    private float fInviTime;
    private bool bHitOnce = false;

   
    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        aAudioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
       
        GameObject SceneManager = GameObject.FindWithTag("SceneManager");
        Scene1 PlayerState = SceneManager.GetComponent<Scene1>();

        fCooldownFireRate--;
        #region Controls Code

        STGEngine.PlayerShipMovement(kLeft, kRight, kUp, kDown, vBotLeftCor, vUpRightCor, bMoveable);   
        STGEngine.PlayerShipSpeedMovement(kBrake, fSpeed);

        //Fire Main
        if (Input.GetKey(kShoot))
        {
            if (fCooldownFireRate < 0)
            {
                STGEngine.ShootOscBullet(rProjectile, 0, 1.0f, 0.2f, 15.0f);
                aAudioSource.PlayOneShot(aBasicShootSound, 1);
                fCooldownFireRate = fOrigFireRate;
            }
        }

        //Fire Bomb // Suicide for now
        if ((Input.GetKeyDown(kSuicide)) && (bBombDeployed == false))
        {
            bBombDeployed = true;
        }

        if (bBombDeployed == true)
        {
            STGEngine.ShootAround(rCircleProjectile, 360, 20.0f);
            iHealth = 0;
            bBombDeployed = false;
        }

        //Shield
        if (fShieldCooldown > 0)
        {
            fShieldCooldown--;
        }
        if ((Input.GetKeyDown(kShield)) && (fShieldCooldown <= 0))
        {
            STGEngine.SpawnPrefab(rShield, this.transform.position, rShield.transform.rotation, 0, 0);
            fShieldCooldown = fShieldCooldownMax;
        }

        //Quit
        if (Input.GetKeyDown(kQuit))
        {
            Quit();
        }
        #endregion


        if ((iHealth <= 0) && (bAlive) && (!GodMode))
        {
            aAudioSource.PlayOneShot(aDestroySound, 1.0f);
            PlayerState.tPlayerState.text = "<color=red>Sys Mal</color>";
            PlayerState.PlayerStateImage.color = Color.red;
            bAlive = false;
        }

        if (!bAlive)
        {
            spin += 5f;
            rThisBody.bodyType = RigidbodyType2D.Dynamic;
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

    void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletScript TargetStatus = Bullet.GetComponent<BulletScript>();
        GameObject SceneManager = GameObject.Find("SceneManager");
        Scene1 PlayerState = SceneManager.GetComponent<Scene1>();

        if ((collision.gameObject.tag == "EnemyBullet") && (!bHitOnce) && (iHealth != 0))
        {
            PlayerState.tPlayerState.text = "<color=red>Sys Cri</color>";
            PlayerState.PlayerStateImage.color = Color.yellow;
            iHealth -= TargetStatus.iBulletDamage;
            aAudioSource.PlayOneShot(aBasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(rFXHit, this.transform.position, this.transform.rotation, 0, 0, 0);
            fInviTime = 30;
            bHitOnce = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletScript TargetStatus = Bullet.GetComponent<BulletScript>();
        GameObject SceneManager = GameObject.Find("SceneManager");
        Scene1 PlayerState = SceneManager.GetComponent<Scene1>();
        GameObject Shield = GameObject.FindWithTag("Shield");

        if ((collision.gameObject.tag == "EnemyBullet") && (!bHitOnce) && (iHealth != 0) && (!Shield))
        {
            PlayerState.tPlayerState.text = "<color=orange>Sys Cri</color>";
            PlayerState.PlayerStateImage.color = Color.yellow;
            iHealth -= TargetStatus.iBulletDamage;
            aAudioSource.PlayOneShot(aBasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(rFXHit, this.transform.position, this.transform.rotation, 0, 0, 0);
            fInviTime = 30;
            bHitOnce = true;
        }
        
        if ((collision.gameObject.tag == "Enemy") && (!bHitOnce) && (iHealth != 0) && (!Shield))
        {
            PlayerState.tPlayerState.text = "<color=orange>Sys Cri</color>";
            PlayerState.PlayerStateImage.color = Color.yellow;
            iHealth -= 10;
            aAudioSource.PlayOneShot(aBasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(rFXHit, this.transform.position, this.transform.rotation, 0, 0, 0);
            fInviTime = 30;
            bHitOnce = true;
        }
    }
}
