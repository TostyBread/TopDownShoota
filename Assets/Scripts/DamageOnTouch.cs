using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // think of it like radio roadcasting
    public delegate void OnHitSomething();
    public OnHitSomething OnHit;

    public float Damage = 1f;
    public float PushForce = 10f;

    public GameObject[] DamagableFeedbacks;
    public GameObject[] AnythingFeedbacks;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If we hit something that doesn't belong in our TargetLayerMask
        if (((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
        {
            HitDamagable(col);
        }
        else
        {
            HitAnything(col);
        }
    }

    private void HitAnything(Collider2D col)
    {
        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        OnHit?.Invoke();
        SpawnFeedbacks(AnythingFeedbacks); 
    }

    private void HitDamagable(Collider2D col)
    {
        Health targetHealth = col.gameObject.GetComponent<Health>();

        if (targetHealth == null)
            return;

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        TryDamage(targetHealth);
        SpawnFeedbacks(DamagableFeedbacks);
    }

    private void TryDamage(Health targetHealth)
    {
        targetHealth.Damage(Damage, transform.gameObject);
        Debug.Log("hit" + targetHealth);
        OnHit?.Invoke();
    }

    void SpawnFeedbacks(GameObject[] Feedbacks)
    {
        foreach(var feedback in Feedbacks)
        {
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }
    }
}
