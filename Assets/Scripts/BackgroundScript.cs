using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public float fScrollDownSpeed = 0.0001f;

    // Use this for initialization
    void Start () {
    	
    }
	
    // Update is called once per frame
    void Update () {
        this.transform.Translate(new Vector3(fScrollDownSpeed, 0));
    }
}
