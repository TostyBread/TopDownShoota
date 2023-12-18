using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lootdrop : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public float DropChance = 10f; // Drop chance

    public GameObject[] RandomLoots; // insert pickups here to spawn when enemy died 
    public HitEvent OnHit;
    private Health _health;

    void Start()
    {
        _health = GetComponent<Health>();

        if (_health == null)
            return;

        _health.OnDeath += DropLoot;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= DropLoot; // when player killed, turn off loopdrop (or "unsubscribe" it)
    }
    void DropLoot()
    {
        float chance = Random.Range(0, 100);

        if (chance > DropChance)
            return;

        int selectedLoot = Random.Range(0, RandomLoots.Length);

        GameObject.Instantiate(RandomLoots[selectedLoot], transform.position, transform.rotation);
    }
}
