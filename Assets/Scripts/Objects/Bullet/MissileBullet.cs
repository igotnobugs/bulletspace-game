using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnUpdateBullet() {
        speed += acceleration;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        /*        while (delay > 0)
        {
            delay = delay - Time.deltaTime;
        }

        if (delay <= 0)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag(targetTag);
            GameObject closest = null;

            foreach (GameObject enemy in enemys)
            {
                Vector3 diff = enemy.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < targetDistanceNeeded)
                {
                    closest = enemy;
                    targetDistance = curDistance;
                }
            }

            if ((closest) && (!missileFired))
            {
                targetLocation = closest.transform.position;
                speed += acceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                if (explodeOnTargetLocation)
                {
                    //Stay on direction, presumable to explode
                    transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
                }
                else
                {
                    //Continue direction
                    //get
                    var direction = targetLocation - transform.position;
                    direction.Normalize();
                    Debug.Log(direction);
                    transform.Translate(direction * speed * Time.deltaTime);
                }
            }
            else
            {
                //No closest enemy then contiue.
                speed += acceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            if (missileFired)
            {
                speed += acceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
            }
        }
        
         
                     /*
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
         */
    }
}
