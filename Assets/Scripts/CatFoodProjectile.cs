using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodProjectile : MonoBehaviour
{
    public LayerMask TargetLayerMask; //Target specific layer
    public Cooldown BulletLife; // Catfood life time (unused)

    public float throwForce = 10f;
    private Rigidbody2D _rigidbody;
    private DamageOnTouch _damageOnTouch;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(transform.up * throwForce, ForceMode2D.Impulse); // Apply initial force to simulate throwing
        _damageOnTouch = GetComponent<DamageOnTouch>();

        // getting rid of timer will avoid hitbox from despawning (due to it being different from normal projectile)
    }

    void Update()
    {
        if (BulletLife.CurrentProgress == Cooldown.Progress.Finished) //if bulletlife is finished, then destroy itself
            Die();
    }

    protected virtual void Die() //bullet destroy itself
    {
        // unsubscribing
        if (_damageOnTouch != null)
            _damageOnTouch.OnHit -= Die;

        BulletLife.StopCooldown();
        Destroy(gameObject);
    }
}
