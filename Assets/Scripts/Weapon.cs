using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject Projectile; //Weapon bullet
    public Transform SpawnPos; //Weapon start position
    public float Interval = 0.1f; //fire rate

    private float ShotFiredSingle = 0f;
    private float _timer = 0f;
    private bool _canShoot = true;

    public enum FireModes //weapon fire modes
    {
        Auto,
        SingleFire,
        BurstFire
    }

    public FireModes FireMode;

    // Update is called once per frame
    void Update()
    {

        switch (FireMode)
        {
            case FireModes.Auto: //full auto uses this
                if (_timer < Interval) 
                {
                    _timer += Time.deltaTime;
                    _canShoot = false;
                    return;
                }
                break;

            case FireModes.SingleFire: //single fire only shoot when left-click
                if (ShotFiredSingle < Interval) 
                {
                    ShotFiredSingle += Time.deltaTime;
                    _canShoot = true;
                    return;
                }
                break;
        }

        ShotFiredSingle = 0;
        _timer = 0f;
        _canShoot = true;
    }

    public void Shoot()
    {
        if(Projectile == null) //DEBUG if projectile is missing
        {
            Debug.LogWarning("Missing Porjectile Prefab");
            return;
        }

        if (SpawnPos == null) //DEBUG if spawn position is mising
        {
            Debug.LogWarning("Missing SpawnPosition transform");
            return;
        }

        if (!_canShoot) //if player cant shoot, do nothing
            return;

        GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation); //The projectile properties (shoot position and direction)
    }
}
