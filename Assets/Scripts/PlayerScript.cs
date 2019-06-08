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
    public Vector2 vBottomLeftCorner, vUpperRightCorner;

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
    private float spin = 0.5f;
    public int iBombAmmo = 2;
    private bool bBombDeployed = false;
    

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

        STGEngine.PlayerShipNotMoving(vBottomLeftCorner, vUpperRightCorner);
        STGEngine.PlayerShipMoveLeft();
        STGEngine.PlayerShipMoveRight();
        STGEngine.PlayerShipMoveDown();
        STGEngine.PlayerShipMoveUp();
        STGEngine.PlayerMovement(fSpeed);


        //Fire Main
        if (Input.GetKey(KeyCode.Space))
        {
            if (fCooldownFireRate < 0)
            {
                STGEngine.ShootOscBullet(rProjectile, 0, 1.0f, 0.2f, 15.0f);
                aAudioSource.PlayOneShot(aBasicShootSound, 1);
                fCooldownFireRate = fOrigFireRate;
            }
        }


        //Fire Bomb
        if ((Input.GetKeyDown(KeyCode.F)) && (bBombDeployed == false))
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
        if ((Input.GetKeyDown(KeyCode.E)) && (fShieldCooldown <= 0))
        {
            STGEngine.SpawnPrefab(rShield, this.transform.position, rShield.transform.rotation, 0, 0);
            fShieldCooldown = 2000;
        }

        ;
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
