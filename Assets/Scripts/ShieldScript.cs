using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject Player = GameObject.FindWithTag("Player");
        this.transform.position = Player.transform.position;
        Destroy(gameObject, 2.0f);
    }

}
