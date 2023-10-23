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

}
