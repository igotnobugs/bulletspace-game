using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Stats")]
    public int shipHealth = 1;
    public bool isAlive = true;
    public float shipSpeed = 10.0f;


    public Weapon mainWeapon;

    // Components
    protected Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int amount) {
        shipHealth -= amount;
        if (shipHealth <= 0) {
            Death();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        //BulletController bullet = collision.gameObject.GetComponent<BulletController>();

        //if (collision.gameObject.CompareTag("EnemyBullet") && (!bHitOnce) && (shipHealth != 0)) {
        //    shipHealth -= bullet.damage;
        //    AudioSource.PlayOneShot(BasicHitSound, 1.0f);
        //    STGEngine.SpawnPrefab(bullet.hitEffect, this.transform.position, this.transform.rotation, new Vector2(0, 0), 0);
        //    fInviTime = 30;
        //    bHitOnce = true;
        //}
    }

    protected virtual void Death() {
        //AudioSource.PlayOneShot(DestroySound, 1.0f);
        //isAlive = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.rotation = Quaternion.Euler(180, 0, 5f + Time.deltaTime);
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        Destroy(gameObject, 1.0f);
    }
}
