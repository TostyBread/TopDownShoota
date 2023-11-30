using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum FireModes
    {
        Auto, // = 0
        SingleFire, // = 1
        BurstFire // = 2
    }

    private bool _burstFiring = false; // Specifically for burst fire code
    private float _lastShootRequestAt;

    public FireModes FireMode; // Declare fire mode
    public float Spread = 0f; // weapon bloom

    public float _timer = 0f; 
    private bool _canShoot = true;
    private bool _singleFireReset = true;

    public int BurstFireAmount = 3;
    public float BurstFireInterval = 0.1f; // interval between bullets in burst fire

    public GameObject Projectile; //Weapon bullet
    public Transform SpawnPos; //Weapon start position
    public Cooldown AutoFireShootInterval; //Weapon rate of fire (tied with Cooldown script)
    public GameObject[] Feedbacks; // giving feedbacks (like sfx after firing a weapon)

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

        if (SpawnPos == null) //DEBUG if spawn position is missing
        {
            Debug.LogWarning("Missing SpawnPosition transform");
            return;
        }

        switch (FireMode)
        {
            case FireModes.Auto:
                {
                    AutoFireShoot();
                    break;
                }
            case FireModes.SingleFire:
                {
                    SingleFireShoot();
                    break;
                }
            case FireModes.BurstFire:
                {
                    BurstFireShoot();
                    break;
                }
        }
        //if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
        //    return;

        //GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation); //The projectile properties (shoot position and direction)

        //AutoFireShootInterval.StartCooldown();

        //SpawnFeedbacks(); // Will spawn the sound
    }

    void AutoFireShoot()
    {

    }

    void SingleFireShoot()
    {
        if (! _canShoot)
            return;

        if (!_singleFireReset)
            return;

        float randomRot = Random.Range(-Spread, Spread);

        GameObject Bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0, 0, randomRot));
        SpawnFeedbacks(); // Will spawn the sound
    }

    void BurstFireShoot()
    {
        if (!_canShoot)
            return;

        if (_burstFiring)
            return;

        if (!_singleFireReset)
            return;

        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        StartCoroutine(BurstFireCo(1f));
    }

    IEnumerator BurstFireCo(float time = 3f)
    {
        _burstFiring = true;
        _singleFireReset = false;

        //if(Time.time - _lastShootRequestAt < BurstFireInterval) // prevents from calling too much co-routine
        //{
        //    yield break;
        //}

        int remainingShots = BurstFireAmount;

        while(remainingShots > 0)
        {
            float randomRot = Random.Range(-Spread, Spread);

            GameObject Bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0, 0, randomRot));
            SpawnFeedbacks(); // Will spawn the sound
            _lastShootRequestAt = Time.time;

            remainingShots--;
            yield return WaitFor(BurstFireInterval);

        }

        Debug.Log("Ended");

        _burstFiring = false;
        AutoFireShootInterval.StartCooldown(); 
    }

    IEnumerator WaitFor(float seconds)
    {
        for (float timer = 0f; timer < seconds; timer += Time.deltaTime)
        {
            yield return null;
        }
    }

    public void StopShoot()
    {
        _singleFireReset = true;
    }

    void SpawnFeedbacks() // Will spawn the sound
    {
        foreach(var feedback in Feedbacks)
        {
            GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
        }
    }
}
