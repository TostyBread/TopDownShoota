using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHand : MonoBehaviour
{

    public Weapon CurrentWeapon; //Weapons
    public Transform GunPosition; //Weapon position

    protected bool _tryShoot = false; //handles whether weapon shoot or not
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleWeapon();
    }

    protected virtual void HandleInput()
    {

    }

    protected virtual void HandleWeapon()
    {
        if (CurrentWeapon == null) //if weapon doesn't exist, it wont do anything
            return;

        CurrentWeapon.transform.position = GunPosition.position;
        CurrentWeapon.transform.rotation = GunPosition.rotation;

        if (_tryShoot) //when firing weapon
        {
            CurrentWeapon.Shoot();
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weapon == null) //if current weapon isnt equip, do nothing
            return;

        CurrentWeapon = weapon;
    }
}
