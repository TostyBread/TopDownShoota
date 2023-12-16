using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHand : MonoBehaviour
{
    public Weapon CurrentWeapon; // Current equip weapon
    public Transform GunPosition; //Weapon position

    protected bool _tryShoot = false; //handles whether weapon shoot or not
    protected bool _tryReload = false;
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

        CurrentWeapon.transform.position = GunPosition.position; // weapon will snap into player's hands
        CurrentWeapon.transform.rotation = GunPosition.rotation;

        if (_tryShoot) //when firing weapon
            CurrentWeapon.Shoot();
        else // stop shooting
            CurrentWeapon.StopShoot();

        //if (_tryReload)
        //    CurrentWeapon.ReloadPart();
    }

    public void EquipWeapon(GameObject equipWeapon) //Weapon equip
    {
        if (equipWeapon == null) //if current weapon isnt equip, do nothing
            return;

        if(CurrentWeapon != null)
        {
            Destroy(CurrentWeapon.gameObject);
        }

        GameObject _weaponGO = GameObject.Instantiate(equipWeapon, GunPosition);
        Weapon weapon = _weaponGO.GetComponent<Weapon>();

        if (weapon == null)
            return;

        CurrentWeapon = weapon;
    }
}
