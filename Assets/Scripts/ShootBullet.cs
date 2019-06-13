using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Rigidbody2D rBullet, Vector2 vDir, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet = Instantiate(rBullet, transform.position, transform.rotation);
        instBullet.velocity = new Vector2(vDir.x * fSpeed, vDir.y * fSpeed);
    }
}
