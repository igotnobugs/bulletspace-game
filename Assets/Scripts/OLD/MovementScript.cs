using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //Move this object
    public void Towards(Vector2 Location, float additionalSpeed = 1.0f)
    {
        this.transform.position = Vector2.MoveTowards(new Vector2(this.transform.position.x, this.transform.position.y), Location, additionalSpeed * Time.deltaTime);
    }
}

