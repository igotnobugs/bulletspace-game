using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleScript : MonoBehaviour {

    //Needed to work
    private STGFramework STGEngine;
    private AudioSource aAudioSource;

    //Object Components
    public Rigidbody2D rFXHit;
    public Rigidbody2D rSmallRubble;
    public bool bThisIsSmallRubble;

    //Object Audio
    public AudioClip aDestroySound; //Destroyed Sound
    public AudioClip aBasicHitSound; //Hit Sound

    //Object Stats
    public int iHealth;
    private bool bAlive = true;


    // Use this for initialization
    void Start () {
        STGEngine = GetComponent<STGFramework>();
        aAudioSource = GetComponent<AudioSource>();
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

        //Destroyed
        if ((iHealth <= 0) && (bAlive))
        {
            aAudioSource.PlayOneShot(aDestroySound, 1.0f);
            STGEngine.ScoretoAddtoCounter(ScoreCounter.tScoreCounter, 1);
            if (!bThisIsSmallRubble)
            {
                STGEngine.SpawnPrefab(rSmallRubble, this.transform.position, this.transform.rotation, new Vector2(1, -1), 1);
                STGEngine.SpawnPrefab(rSmallRubble, this.transform.position, this.transform.rotation, new Vector2(-1, -1), 1);
            }
            bAlive = false;
        }

        if (!bAlive)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 0.1f);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Bullet = collision.gameObject;
        //BulletScript TargetStatus = Bullet.GetComponent<BulletScript>();
        /*
        if (collision.gameObject.tag == "FriendlyBullet")
        {
            iHealth -= TargetStatus.iBulletDamage;
            aAudioSource.PlayOneShot(aBasicHitSound, 0.6f);
            STGEngine.SpawnPrefab(rFXHit, Bullet.transform.position, Bullet.transform.rotation, new Vector2(0, 0), 0);
        }

        if (collision.gameObject.tag == "Shield")
        {
            iHealth = -100;
            aAudioSource.PlayOneShot(aBasicHitSound, 1.0f);
            STGEngine.SpawnPrefab(rFXHit, Bullet.transform.position, Bullet.transform.rotation, new Vector2(0, 0), 0);
        }*/
    }
}
