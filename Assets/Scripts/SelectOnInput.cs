using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {


    public EventSystem eventSystem;
    public GameObject selectedGameObject;

    private bool bButtonSelected;


    // Use this for initialization
    void Start () {
        bButtonSelected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetAxisRaw("Vertical") != 0) && (bButtonSelected = false))
        {
            eventSystem.SetSelectedGameObject(selectedGameObject);
            bButtonSelected = true;
        }
    }


    private void OnDisable()
    {
        bButtonSelected = false;
    }
}
