using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript: MonoBehaviour
{
    //Requirements
    private STGFramework STGEngine;
    private AIFramework AIFrames;
    private AudioSource aAudioSource;

    //Bullet Components
    public Rigidbody2D rBullet;
    public Rigidbody2D rExplosion;

    //Bullet Stat
    public bool bMissile;
    public float fBulletSpeed;
    public int iBulletDamage = 10;

    //Bullet Audio
    public AudioClip aReadySound;
    public AudioClip aShootSound;

    //For Missile Vars
    public string sTarget;
    private Vector2 vLockon;
    private bool bMissileDeployedSound = false;
    private bool bMissileShootSound = false;     

    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        AIFrames = GetComponent<AIFramework>();
        aAudioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

        GameObject SceneManager = GameObject.FindWithTag("SceneManager");
        Scene1 ResetOnGoing = SceneManager.GetComponent<Scene1>();

        //Reset
        if (ResetOnGoing.bResetOnGoing == true)
        {
            Destroy(gameObject);
        }

        if (bMissile) //A Missile Type
        {
            AIFrames.EventAIStart();
            switch (AIFrames.iMovementStage)
            {
                case 1:
                    STGEngine.ShootAiming("Player");
                    GameObject Target = GameObject.Find("Player");
                    if (Target)
                    {
                        vLockon = new Vector2(Target.transform.position.x, Target.transform.position.y);
                    } else
                    {
                        //Debug.Log("No Target");
                    }
                    if (!bMissileDeployedSound)
                    {
                        aAudioSource.PlayOneShot(aReadySound, 2);
                        bMissileDeployedSound = true;
                    }
                    AIFrames.EventMoveEnd(2);
                    break;
                case 2:
                    if (!bMissileShootSound)
                    {
                        aAudioSource.PlayOneShot(aShootSound, 2);
                        bMissileShootSound = true;
                    }
                    STGEngine.MoveTowards(vLockon, 10.0f);
                    if ((this.transform.position.x == vLockon.x) && (this.transform.position.y == vLockon.y))
                    {
                        STGEngine.ShootAround(rExplosion, 10, 5.0f);
                        Destroy(gameObject);
                    }
                    break;
            }                  
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 5);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "FriendlyBullet")
        {
            if (collision.gameObject.tag == "Enemy")
            {               
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "EnemyBullet")
        {
            if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "Shield"))
            {
                Destroy(gameObject);
            }
        }
    }
}
