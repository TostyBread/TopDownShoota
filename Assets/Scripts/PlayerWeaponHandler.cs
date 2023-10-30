using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : WeaponHand
{
    protected override void HandleInput()
    {
        if (Input.GetButton("Fire1"))
            _tryShoot = true;

        if (Input.GetButtonUp("Fire1"))
            _tryShoot = false;
    }
}
