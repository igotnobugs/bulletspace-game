using UnityEngine;


public class ShootingMechanics : MonoBehaviour
{

    private float fShootTimeAc = 0;
    //private float fShootSoundTimeAc = 0;
    private float fAngle = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Standard(Rigidbody2D rBullet, Vector2 vDir, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet = Instantiate(rBullet, this.transform.position, this.transform.rotation);
        instBullet.velocity = new Vector2(vDir.x * fSpeed, vDir.y * fSpeed);
    }

    public void Around(Rigidbody2D rBullet, int iBulletCount, float fSpeed = 1.0f, int iReps = 1)
    {
        for (float angle = 0.0f; angle < 360.0f; angle += (360.0f / iBulletCount))
        {
            var rad = angle * Mathf.Deg2Rad;
            Vector2 vDir;
            vDir.x = Mathf.Sin(rad);
            vDir.y = Mathf.Cos(rad);

            Standard(rBullet, vDir, fSpeed);
        }
    }

    public void AroundSwirl(Rigidbody2D rBullet, int iBulletCount, float fFireRate = 0.02f, float fSpeed = 1.0f, int iMaxRots = 1, bool bReverse = false)
    {
        if (Time.deltaTime + fShootTimeAc > fFireRate)
        {
            fShootTimeAc = 0.0f;
            if (!bReverse)
            {
                fAngle += (360.0f / iBulletCount);
            }
            else
            {
                fAngle -= (360.0f / iBulletCount);
            }

            var rad = fAngle * Mathf.Deg2Rad;

            Vector2 vDir;
            vDir.x = Mathf.Sin(rad);
            vDir.y = Mathf.Cos(rad);

            Standard(rBullet, vDir, fSpeed);
        }
        else
        {
            fShootTimeAc += Time.deltaTime;
        }
        //Debug.Log(fShootTimeAc);
    }
}