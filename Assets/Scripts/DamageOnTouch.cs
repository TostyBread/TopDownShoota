using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    //think of it like radio roadcasting
    public delegate void OnHitSomething();
    public OnHitSomething OnHit;

    public float Damage = 1f;
    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //IF we hit something that doesn't belong in our TargetLayerMask
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        Health targetHealth = col.gameObject.GetComponent<Health>();

        if (targetHealth == null)
            return;

        TryDamage(targetHealth);
    }

    private void TryDamage(Health targetHealth)
    {
        targetHealth.Damage(Damage, transform.parent.gameObject);
        Debug.Log("hit" + targetHealth);
        OnHit?.Invoke();
    }
}
