using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Damage = 1f; //bullet damage
    public float Speed = 100f; //bullet speed
    public float PushForce = 50f; //knockback distance
    public float lifeTime = 1f; //bullet lifetime before despawn

    public LayerMask TargetLayerMask;

    private Rigidbody2D _rigidbody;
    private float _timer = 0f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody == null)
            return;

        _rigidbody.AddRelativeForce(new Vector2(0f, Speed));
    }

    void Update()
    {
        if (_timer < lifeTime) //When bullet timer runs out
        {
            _timer += Time.deltaTime;
            return;
        }

        Die();
    }


    protected virtual void Die() //bullet destroy itself
    {
        Destroy(gameObject);
    }
}
