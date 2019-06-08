using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript: MonoBehaviour {

    private STGFramework STGEngine;
    private EVFramework EVFrames;
    private AudioSource aAudioSource;

    public Rigidbody2D rBullet;
    public Rigidbody2D rExplosion;



    public float fBulletSpeed;
    public string sTarget;
    public bool bMissile;

    private bool bMissileDeployedSound = false;
    private bool bMissileShootSound = false;

    private Vector2 vLockon;

    public AudioClip aReadySound;
    public AudioClip aShootSound;

    //Bullet Stats
    public int iBulletDamage = 10;

    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        EVFrames = GetComponent<EVFramework>();
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
            EVFrames.EventAIStart();
            switch (EVFrames.iMovementStage)
            {
                case 1:
                    STGEngine.ShootAiming("Player");
                    GameObject Target = GameObject.Find("Player");
                    vLockon = new Vector2(Target.transform.position.x, Target.transform.position.y);
                    if (!bMissileDeployedSound)
                    {
                        aAudioSource.PlayOneShot(aReadySound, 2);
                        bMissileDeployedSound = true;
                    }
                    EVFrames.EventMoveEnd(2);
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
