using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public float delayToOnInvinsible = 2.0f;
    public float delayToFire = 2.0f;

    //private bool startDetectOnInvinsible = false;

    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(180, 0, 0);
        //StartCoroutine(CountdownToInvinsible());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * shipSpeed * Time.deltaTime);

        /*
                if (faceThePlayer)
        {
            GameObject Target = GameObject.FindWithTag("Player");
            if (Target)
            {
                PlayerController targetscript = Target.GetComponent<PlayerController>();
                float targetspeed = targetscript.shipSpeed;
                Vector2 targetdirection = targetscript.playerDirection;
                float xpos = Target.transform.position.x + (targetscript.playerDirection.x * targetscript.shipSpeed) - this.transform.position.x;
                float ypos = Target.transform.position.y + (targetscript.playerDirection.y * targetscript.shipSpeed) - this.transform.position.y;
                float angle = Mathf.Atan2(ypos, xpos) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }*/
    }

    /*IEnumerator CountdownToInvinsible() {
        yield return new WaitForSeconds(delayToOnInvinsible);

        startDetectOnInvinsible = true;

        yield return null;
    }*/

    private void OnBecameIn() {
        //if (!startDetectOnInvinsible) return;

        Destroy(gameObject, 1.0f);
    }
}
