using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAround : MonoBehaviour
{
    // Start is called before the first frame update
    int rep;

    void Start()
    {
        rep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Rigidbody2D rBullet, int iBulletCount, float fSpeed = 1.0f, int iReps = 1)
    {
        if (rep < iReps)
        {
            for (float angle = 0.0f; angle < 360.0f; angle += (360.0f / iBulletCount))
            {
                var rad = angle * Mathf.Deg2Rad;
                Vector2 vDir;
                vDir.x = Mathf.Sin(rad);
                vDir.y = Mathf.Cos(rad);
                Rigidbody2D instBullet = Instantiate(rBullet, transform.position, transform.rotation);
                instBullet.velocity = new Vector2(vDir.x * fSpeed, vDir.y * fSpeed);
            }
            rep++;
        }
    }
}
