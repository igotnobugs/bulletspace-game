using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [HideInInspector]
    public bool isFiring, fireLeft = true;

    public BulletController bullet;
    public PlayerController player;
    public EnemyController enemy;

    public bool isPlayerGun = false;

    public float bulletSpeed = 10;
    public float fireRate = 100;

    private float shotCounter;

    private AudioSource AudioSource;

    public bool isOscilatingGun;

    public Transform firePoint1;
    public AudioClip ShootSound1;

    public Transform firePoint2;
    public AudioClip ShootSound2;

    private Vector2 fireLocation;
    private Quaternion fireRotation;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        if (isPlayerGun)
        {
            fireRate = player.FIRERATE_MAX;
        } else
        {
            fireRate = enemy.FIRERATE_MAX;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }



        if (isFiring)
        {
            if (shotCounter <= 0)
            {
                shotCounter = 1 - (fireRate/100);

                //If Gun Oscilates between two firing positions else defaults to one.
                if (isOscilatingGun)
                {
                    if (fireLeft)
                    {
                        fireLocation = firePoint1.position;
                        fireRotation = firePoint1.rotation;
                        fireLeft = false;
                    }
                    else
                    {
                        fireLocation = firePoint2.position;
                        fireRotation = firePoint2.rotation;
                        fireLeft = true;
                    }
                } else
                {
                    fireLocation = firePoint1.position;
                    fireRotation = firePoint1.rotation;
                }

                BulletController newbullet = Instantiate(bullet, fireLocation, fireRotation) as BulletController;
                newbullet.speed = bulletSpeed;
                newbullet.tag = tag;
                AudioSource.PlayOneShot(ShootSound1, 1);

                //set player specific variables
                if (isPlayerGun)
                {

                    newbullet.targetTag = "Enemy";
                    newbullet.bulletType = "missilelockson";
                    newbullet.targetDistanceNeeded = 99.0f;

                }

                //player.heatSink += player.mainHeatCost;
            }
            //Debug.Log(shotCounter);
        }
    }
}
