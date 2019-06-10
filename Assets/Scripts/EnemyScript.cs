using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Needed to work
    private STGFramework STGEngine;
    private AIFramework AIFrames;
    private AudioSource aAudioSource;

    //Ship Components
    public Rigidbody2D rThisBody; //RigidBody of the gameObject
    public Rigidbody2D rMainProjec; //Main Projectile of the Ship
    public Rigidbody2D rFXHit;

    //Ship Audio
    public AudioClip aBasicShootSound; //Shooting Sound
    public AudioClip aBasicHitSound; //Hit Sound
    public AudioClip aDestroySound; //Destroyed Sound

    //Ship Stats
    public int iHealth;
    private float fCooldownFireRate;
    public float fShipFireRate;
    private float fDeathSpin = 1.0f;
    private bool bAlive = true;

    // Use this for initialization
    void Start () {
        STGEngine = GetComponent<STGFramework>();
        AIFrames = GetComponent<AIFramework>();

        aAudioSource = GetComponent<AudioSource>();
        this.transform.rotation = Quaternion.Euler(180, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject SceneManager = GameObject.FindWithTag("SceneManager");
        Scene1 ScoreCounter = SceneManager.GetComponent<Scene1>();
        Scene1 ResetOnGoing = SceneManager.GetComponent<Scene1>();

        //Reset
        if (ResetOnGoing.bResetOnGoing == true)
        {
            Destroy(gameObject);
        }

        AIFrames.EventAIStart();
        switch (AIFrames.iMovementStage)
        {
            case 1:
                if (iHealth > 0)
                {
                    STGEngine.ShootConBullet(rMainProjec, new Vector2(0, -1.0f), fShipFireRate, 5.0f);
                    STGEngine.ShootSoundBullet(aBasicShootSound, fShipFireRate);
                }
                break;
        }

        if ((this.transform.position.y <= 1))
        {
            Destroy(gameObject);
        }

        if ((iHealth <= 0) && (bAlive))
        {      
            aAudioSource.PlayOneShot(aDestroySound, 1.0f);
            STGEngine.ScoretoAddtoCounter(ScoreCounter.tScoreCounter, 5);
            bAlive = false;
        }

        if (!bAlive)
        {
            fDeathSpin += 5f;
            rThisBody.bodyType = RigidbodyType2D.Dynamic;
            this.transform.rotation = Quaternion.Euler(180, 0, fDeathSpin);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 1.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletScript TargetStatus = Bullet.GetComponent<BulletScript>();

        if (collision.gameObject.tag == "FriendlyBullet")
        {              
            iHealth -= TargetStatus.iBulletDamage;
            aAudioSource.PlayOneShot(aBasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(rFXHit, Bullet.transform.position, Bullet.transform.rotation, new Vector2(0, 0), 0);
        }

        if (collision.gameObject.tag == "Shield")
        {
            iHealth = -100;
            aAudioSource.PlayOneShot(aBasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(rFXHit, Bullet.transform.position, Bullet.transform.rotation, new Vector2(0, 0), 0);
        }

    }
}
