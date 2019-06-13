using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject PlayerObject = GameObject.Find("Player");
        Image PlayerImage = this.GetComponent<Image>();

        if (!PlayerImage)
        {
            Debug.Log("Missing Image Component");
        }

        if (PlayerObject)
        {
            PlayerController Player = PlayerObject.GetComponent<PlayerController>();
            if (Player.isAlive == true)
            {
                if (Player.shipHealth == Player.SHIP_HEALTH_MAX)
                {
                    PlayerImage.color = Color.cyan;
                }

                if (Player.shipHealth < Player.SHIP_HEALTH_MAX)
                {
                    PlayerImage.color = Color.yellow;
                }
            }
            else
            {
                PlayerImage.color = Color.red;
            }
        }
    }
}
