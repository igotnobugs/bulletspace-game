using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject PlayerObject = GameObject.Find("Player");
        Text PlayerState = this.GetComponent<Text>();

        if (!PlayerState)
        {
            Debug.Log("Missing Text Component");
        }

        if (PlayerObject)
        {
            PlayerShip Player = PlayerObject.GetComponent<PlayerShip>();
            if (Player.isAlive == true)
            {
                /*if (Player.shipHealth == Player.SHIP_HEALTH_MAX)
                {
                    PlayerState.color = Color.cyan;
                    PlayerState.text = "Sys Nom";
                }

                if (Player.shipHealth < Player.SHIP_HEALTH_MAX)
                {
                    PlayerState.color = Color.yellow;
                    PlayerState.text = "Sys Cri";
                }*/
            }
            else
            {
                PlayerState.color = Color.red;
                PlayerState.text = "Sys Mal";
            }
        }
    }
}
