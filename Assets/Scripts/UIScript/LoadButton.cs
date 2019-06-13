using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour {


    public Button loadButton;

    void Update()
    {
        //Toggle interactable state of the Button on and off
        loadButton.interactable = !loadButton.interactable;       
    }  
}
