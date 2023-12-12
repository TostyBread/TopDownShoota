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
    private bool _singleFireReset = true; // Simgle fire toggle

    public int BurstFireAmount = 3; // bullets shoot in each burst fire
    public float BurstFireInterval = 0.1f; // interval between bullets in burst fire
    public int ProjectileCount = 10; // initial amount of bullet in a fresh picked up weapon?

    public GameObject Projectile; //Weapon bullet

    public Transform SpawnPos; //Weapon start position
    public Cooldown ShootInterval; //Weapon rate of fire (tied with Cooldown script)

    public GameObject[] Feedbacks; // giving feedbacks (like sfx after firing a weapon)
    public GameObject[] ReloadFeedbacks;

    public Cooldown ReloadCooldown;
    public int MaxBulletCount = 20;

    public int CurrentBulletCount // bullet system
    {
        get { return currentBulletCount; }
    }
    protected int currentBulletCount;

    private void Start()
    {
        
    }
    void Update()
    {
        UpdateReloadCooldown();
        UpdateShootCooldown();
    }

    private void UpdateReloadCooldown()
    {
        if (ReloadCooldown.CurrentProgress != Cooldown.Progress.Finished) //if weapon is not finish shooting, return
            return;

        if (ReloadCooldown.CurrentProgress == Cooldown.Progress.Finished)
        {
            currentBulletCount = MaxBulletCount;
        }

        ReloadCooldown.CurrentProgress = Cooldown.Progress.Ready; //if weapon is finish shooting, ready to fire next time user shoots.
    }

    private void UpdateShootCooldown()
    {
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        ShootInterval.CurrentProgress = Cooldown.Progress.Ready;
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

        if (ReloadCooldown.IsOnCooldown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready)
            return;

        switch (FireMode) // Weapon fire modes
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

    }

    void ShootProjectile() // handles the projectile part
    {
        float randomRot = Random.Range(-Spread, Spread);

        GameObject Bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0, 0, randomRot));
        SpawnFeedbacks(); // Will spawn the sound
    }

    void ReloadStart() // ReloadCheck will handle the ammunition decreasing and reloading part
    {

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCooldown) // when empty, reload timer will start
        {
            foreach (var feedback in ReloadFeedbacks)
            {
                GameObject.Instantiate(feedback, transform.position, transform.rotation);
            }

            ReloadCooldown.StartCooldown();
        }
    }

    void AutoFireShoot() // Handles Auto fire mode
    {
        if (!_canShoot)
            return;

        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        ShootProjectile(); // Shoots bullet
        currentBulletCount--; // when shoot, bullet will decrease
        ShootInterval.StartCooldown();

        ReloadStart(); // check if gun is empty
        
    }

    void SingleFireShoot() // Handles Single fire mode
    {
        if (! _canShoot)
            return;

        if (!_singleFireReset)
            return;

        ShootProjectile();
        currentBulletCount--; // when shoot, bullet will decrease

        ReloadStart(); // check if gun is empty

        _singleFireReset = false;
    }

    void BurstFireShoot()
    {
        if (!_canShoot)
            return;

        if (_burstFiring)
            return;

        if (!_singleFireReset)
            return;

        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        StartCoroutine(BurstFireCo());
    }

    IEnumerator BurstFireCo(float time = 3f)
    {
        _burstFiring = true;
        _singleFireReset = false;

        int remainingShots = BurstFireAmount;

        while(remainingShots > 0)
        {
            ShootProjectile();
            currentBulletCount--; // when shoot, bullet will decrease
            ReloadStart(); // Check if gun is empty

            _lastShootRequestAt = Time.time;

            remainingShots--;
            yield return WaitFor(BurstFireInterval);

        }

        _burstFiring = false;
        ShootInterval.StartCooldown(); 
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
        if (FireMode == FireModes.Auto)
            return;

        _singleFireReset = true;
    }

    void SpawnFeedbacks() // Will spawn the sound
    {
        foreach ( var feedback in Feedbacks )
        {
            GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
        }
    }
}
