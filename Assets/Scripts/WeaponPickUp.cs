using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : Pickup
{
    public GameObject Weapon;

    protected override void PickedUp(Collider2D col)
    {
        WeaponHand weaponHand = col.GetComponent<WeaponHand>();

        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        weaponHand.EquipWeapon(Weapon);
    }
}
