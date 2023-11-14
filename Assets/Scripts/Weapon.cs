using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject Projectile; //Weapon bullet
    public Transform SpawnPos; //Weapon start position
    public Cooldown AutoFireShootInterval; //Weapon rate of fire (tied with Cooldown script)


    void Update()
    {
        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Finished) //if weapon is not finish shooting, return
            return;

        AutoFireShootInterval.CurrentProgress = Cooldown.Progress.Ready; //if weapon is finish shooting, ready to fire next time user shoots.
    }

    public void Shoot()
    {
        if (Projectile == null) //DEBUG if projectile is missing
        {
            Debug.LogWarning("Missing Porjectile Prefab");
            return;
        }

        if (SpawnPos == null) //DEBUG if spawn position is mising
        {
            Debug.LogWarning("Missing SpawnPosition transform");
            return;
        }


        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation); //The projectile properties (shoot position and direction)

        AutoFireShootInterval.StartCooldown();
    }
}
