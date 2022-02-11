using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Stats")]
    public Bullet bullet;
    public float fireRate = 1.0f;

    [Header("Setup")]
    public Transform[] barrels;
    public bool roundRobin = false;

    //private bool isShooting = false;
    private int barrelIndex = 0;
    private float fireCooldown = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
    }

    public virtual void Shoot() {
        if (fireCooldown > 0) return;

        fireCooldown = 1.0f / fireRate;
   
        if (roundRobin) {
            ShootOnce();         
        } else {
            ShootAll();
        }     
    }

    private void ShootOnce() {
        int barrelToUse = barrelIndex % barrels.Length;
        Bullet newBullet = Instantiate(bullet, barrels[barrelToUse].position, barrels[barrelToUse].rotation);
        barrelIndex++;
    }

    private void ShootAll() {
        for (int i = 0; i < barrels.Length; i++) {
            Bullet newBullet = Instantiate(bullet, barrels[i].position, barrels[i].rotation);
        }

    }
}
