using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class PlayerShip : Ship
{

    private  Vector2 playerDirection;
    public bool controlled = true;

    public Controls playerControls;
    public bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = new Controls();
        
        playerControls.Default.Fire.performed += Fire_performed;
        playerControls.Default.Fire.canceled += Fire_performed;

        playerControls.Default.Move.performed += Move_performed;
        playerControls.Default.Move.canceled += Move_performed;
        playerControls.Enable();
    }

    private void Move_performed(InputAction.CallbackContext obj) {
        if (obj.performed) {
            playerDirection = playerControls.Default.MoveDirection.ReadValue<Vector2>();
            rb.velocity = playerDirection * shipSpeed;
        } else {
            rb.velocity = playerDirection * 0;
        }

    }
    private void Fire_performed(InputAction.CallbackContext obj) {
        if (obj.canceled) {
            isShooting = false;
        } else {
            isShooting = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting && isAlive) {
            mainWeapon.Shoot();
        }
    }

}
