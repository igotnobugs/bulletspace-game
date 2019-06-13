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
                if (fireLeft)
                {
                    BulletController newbullet = Instantiate(bullet, firePoint1.position, firePoint1.rotation) as BulletController;
                    newbullet.speed = bulletSpeed;
                    newbullet.tag = tag;
                    if (isOscilatingGun)
                    {
                        fireLeft = false;
                    }
                } else
                {
                    BulletController newbullet = Instantiate(bullet, firePoint2.position, firePoint2.rotation) as BulletController;
                    newbullet.speed = bulletSpeed;
                    newbullet.tag = tag;
                    fireLeft = true;
                }
                AudioSource.PlayOneShot(ShootSound1, 1);
                //player.heatSink += player.mainHeatCost;
            }
            //Debug.Log(shotCounter);
        }
    }
}
