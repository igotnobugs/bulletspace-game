using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDown : MonoBehaviour
{
    public float ScrollDownSpeed = 0.0001f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        this.transform.Translate(new Vector3(ScrollDownSpeed, 0));
    }
}
