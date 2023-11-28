using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Speed = 100f; //bullet speed
    public Cooldown BulletLife; //bullet lifetime before despawn
    
    public LayerMask TargetLayerMask;

    private Rigidbody2D _rigidbody;
    private DamageOnTouch _damageOnTouch;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddRelativeForce(new Vector2(0f, Speed));
        _damageOnTouch = GetComponent<DamageOnTouch>();

        // subscribing
        if (_damageOnTouch != null)
            _damageOnTouch.OnHit += Die;

        BulletLife.StartCooldown(); //everytime bullet spawns, the timer is tied with it
    }

    void Update()
    {
        if (BulletLife.CurrentProgress == Cooldown.Progress.Finished) //if bulletlife is finsied, then destroy itself
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
