using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    private Transform joystick; 
    private Transform character; 
    private bool isMoving = false; 

    void Update()
    {
        if (joystick != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            joystick.position = Camera.main.ScreenToWorldPoint(mousePosition);
            isMoving = true;
        }

        if (isMoving)
        {
            
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; 
            joystick.position = Camera.main.ScreenToWorldPoint(mousePosition);

         
            Vector3 moveDirection = joystick.position - character.position;
            moveDirection.z = 0; 
            moveDirection.Normalize(); 

            character.Translate(moveDirection * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }
}