using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit; // When hit

    public delegate void HealEvent(GameObject source);
    public HealEvent OnHeal; // Healing event

    public delegate void ResetEvent();
    public ResetEvent OnHitReset; // after Hit

    public delegate void DeathEvent(); // When enemy dies
    public DeathEvent OnDeath;

    public float MaxHealth = 10f;
    public Cooldown Invulnerable; // Player invulnerable

    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    private float _currentHealth = 10f;
    private bool _canDamage = true;

    
    void Update()
    {
        ResetInvulnerable();
    }

    private void ResetInvulnerable()
    {
        if (_canDamage)
            return;

        if (Invulnerable.IsOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
    }
    public void Damage(float damage, GameObject source)
    {
        if (!_canDamage)
            return;
        _currentHealth -= damage;

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Die();
        }

        if (Invulnerable.Duration > 0)
        {
            Invulnerable.StartCooldown();
            _canDamage = false;

        }

        Invulnerable.StartCooldown();
        _canDamage = false;

        OnHit?.Invoke(source);
    }

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);

        OnHit?.Invoke(gameObject);
    }

    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(this.gameObject);
    }
    
}
