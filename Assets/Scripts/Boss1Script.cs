using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss1Script : MonoBehaviour
{
    //Needed to work
    private STGFramework STGEngine;
    private AIFramework AIFrames;
    private AudioSource aAudioSource;

    //Ship Components
    public Rigidbody2D rThisBody; //RigidBody of the gameObject
    public Rigidbody2D rMainProjec; //Main Projectile of the Ship
    public Rigidbody2D rMissileProjec;
    public Rigidbody2D rFXHit;

    //Ship Audio
    public AudioClip aShootSound; //Shooting Sound
    public AudioClip aBasicHitSound; //Hit Sound
    public AudioClip aDestroySound; //Destroyed Sound

    //Ship Stats
    public float iMaxHealth;
    public float iHealth;
    private float fCooldownFireRate;
    public float fOrigFireRate;
    public bool bAlive = true;
    private float fDeathSpin = 1.0f;

    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        AIFrames = GetComponent<AIFramework>();
        aAudioSource = GetComponent<AudioSource>();
        this.gameObject.name = "Boss";
    }

    // Update is called once per frame
    void Update()
    {
        MovementScript Move = gameObject.AddComponent<MovementScript>();
        ShootingMechanics Shoot = gameObject.AddComponent<ShootingMechanics>();

        GameObject SceneManager = GameObject.FindWithTag("SceneManager");
        Scene1 ScoreCounter = SceneManager.GetComponent<Scene1>();
        Scene1 ResetOnGoing = SceneManager.GetComponent<Scene1>();

        //Reset
        if (ResetOnGoing.bResetOnGoing == true)
        {
            Destroy(gameObject);
        }

        fCooldownFireRate--;
        AIFrames.EventAIStart();
        if (iHealth > 0)
        {
            switch (AIFrames.iMovementStage)
            {
                case 1:
                    Move.Towards(new Vector2(0, 0), 1);
                    Shoot.AroundSwirl(rMainProjec, 10, 4);
                    AIFrames.EventMoveEnd(12.0f);
                    break;
                case 2:
                    Move.Towards(new Vector2(-2.0f, 0), 1);
                    STGEngine.ShootAroundSwirl(rMainProjec, 20, 0.05f, 4, 1, false);
                    AIFrames.EventMoveEnd(2.0f);
                    break;
                case 3:
                    Shoot.Around(rMainProjec, 20, 4);
                    AIFrames.EventMoveEnd(0.0f);
                    break;
                case 4:
                    STGEngine.MoveTowards(new Vector2(2.0f, 0), 1);
                    STGEngine.ShootAroundSwirl(rMainProjec, 20, 0.05f, 4, 1, true);
                    AIFrames.EventMoveEnd(2.0f);
                    break;
                case 5:
                    STGEngine.ShootAround(rMainProjec, 20, 4, 1);
                    AIFrames.EventMoveEnd(2.0f);
                    break;
                case 6:
                    STGEngine.ShootAimed(rMainProjec, "Player", 10);
                    AIFrames.EventMoveEnd(2.0f);
                    break;
                case 7:
                    //STGEngine.ShootAimedLeading(rMainProjec, "Player", 10);
                    AIFrames.EventMoveEnd(2.0f);
                    break;
                case 8:
                    STGEngine.MoveTowards(new Vector2(0, 1), 1);
                    AIFrames.EventMoveEnd(2.0f);
                    break;
                case 9:
                    STGEngine.ShootAroundSwirl(rMissileProjec, 20, 0.1f, 1, 1, false);
                    AIFrames.EventMoveEnd(1.0f);
                    break;
                case 10:
                    AIFrames.EventMoveEnd(5.0f);
                    break;
                case 11:
                    AIFrames.EventMoveStart(2);
                    break;
            }
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
            fDeathSpin += 5f;
            rThisBody.bodyType = RigidbodyType2D.Dynamic;
            this.transform.rotation = Quaternion.Euler(180, 0, fDeathSpin);
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            Destroy(gameObject, 1.0f);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletController TargetStatus = Bullet.GetComponent<BulletController>();
        if (collision.gameObject.tag == "FriendlyBullet")
        {
            //iHealth -= TargetStatus.iBulletDamage;
            aAudioSource.PlayOneShot(aBasicHitSound, 0.8f);
            STGEngine.SpawnPrefab(rFXHit, Bullet.transform.position, Bullet.transform.rotation, new Vector2(0, 0) , 0);
        }
    }
}
