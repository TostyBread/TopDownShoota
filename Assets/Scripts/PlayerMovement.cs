using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{

    protected override void HandleInput() //player input here
    {
        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(_inputDirection.x = Input.GetAxis("Horizontal"), _inputDirection.y = Input.GetAxis("Vertical")); //movement direction
    }

    // basically top down shooter with mouse aim code:

    protected override void HandleRotation()
    {
        if (_weaponHandler == null || _weaponHandler.CurrentWeapon== null)
        {
            base.HandleRotation(); 
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Mouse position tied to in game world unit instead of raw mouse position

        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z); // Convert mouse position into a 3d unit position

        Vector2 direction = mousePos - transform.position; // Direction

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f; // Change from radius to degree angle thing

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
