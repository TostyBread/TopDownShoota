using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : WeaponHand
{

    public Transform AimOffset;

    protected override void HandleInput()
    {
        if (Input.GetButton("Fire1"))
            _tryShoot = true;

        if (Input.GetButtonUp("Fire1"))
            _tryShoot = false;
    }

    public Vector2 AimPosition()
    {
        if (CurrentWeapon != null)
            return new Vector2(transform.position.x, transform.position.y); // if we have weapon, then aim offset is available

        return new Vector2(AimOffset.position.x, AimOffset.position.y); //if we dont have weapon, then aim offset is not available
    }
}
