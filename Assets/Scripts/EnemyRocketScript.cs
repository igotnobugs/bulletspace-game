﻿using UnityEngine;

public class EnemyRocketScript : MonoBehaviour {

    //Needed to work
    private STGFramework STGEngine;
    private AIFramework AIFrames;
    private float fCooldownFireRate;
    private AudioSource aAudioSource;


    //Ship Components
    public Rigidbody2D rThisBody; //RigidBody of the gameObject
    public Rigidbody2D rMainProjec; //Main Projectile of the Ship
    public Rigidbody2D rSecondProjec;
    public Rigidbody2D rFXHit;

    //Ship Audio
    public AudioClip aBasicShootSound; //Shooting Sound
    public AudioClip aBasicHitSound; //Hit Sound
    public AudioClip aDestroySound; //Destroyed Sound

    //Ship Stats
    public int iHealth;
    public float fShipFireRate;
    private float spin = 1.0f;
    private bool bAlive = true;


    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        AIFrames = GetComponent<AIFramework>();

        aAudioSource = GetComponent<AudioSource>();
        this.transform.rotation = Quaternion.Euler(180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
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
                    STGEngine.ShootConBullet(rMainProjec, new Vector2(0, 0), fShipFireRate);
                    AIFrames.EventMoveEnd(fShipFireRate + 1);
                }
                break;
            case 2:
                if (iHealth > 0)
                {
                    STGEngine.ShootConAimed(rSecondProjec, "Player", 1);
                    STGEngine.ShootSoundBullet(aBasicShootSound, 1);
                    AIFrames.EventMoveEnd(5.0f);
                }
                break;
            case 3:
                if (iHealth > 0)
                {
                    STGEngine.ShootAround(rSecondProjec, 10, 5, 1);
                    STGEngine.ShootSoundBullet(aBasicShootSound, 1);
                    AIFrames.EventMoveEnd(1.0f);
                }
                break;
            case 4:
                AIFrames.EventMoveStart(1);
                break;
        }

        //Out of Bounds
        if ((this.transform.position.y <= 1))
        {
            Destroy(gameObject);
        }

        //Destroyed
        if ((iHealth <= 0) && (bAlive))
        {
            aAudioSource.PlayOneShot(aDestroySound, 1.0f);
            STGEngine.ScoretoAddtoCounter(ScoreCounter.tScoreCounter, 5);
            bAlive = false;
        }
        if (!bAlive)
        {
            spin += 5f;
            rThisBody.bodyType = RigidbodyType2D.Dynamic;
            this.transform.rotation = Quaternion.Euler(180, 0, spin);
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
