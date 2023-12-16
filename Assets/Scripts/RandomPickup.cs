using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class RandomPickup : MonoBehaviour
{
    public GameObject[] pickupItems; // array of different pickups
    public Transform SpawnPoint; // spawn point position
    public float SpawnInterval = 30f; // time interval between spawns

    //public LayerMask TargetLayerMask;
    //public LayerMask IgnoreLayerMask;

    void Start()
    {
        StartCoroutine(SpawnPickupRoutine());
    }

    IEnumerator SpawnPickupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnInterval);

            // check if there's already a pickup at the spawn point
            Collider2D existingPickup = Physics2D.OverlapCircle(SpawnPoint.position, 0.2f);
            if(existingPickup == null )
            {
                SpawnRandomPickup();
            }
        }
    }

    void SpawnRandomPickup()
    {
        if (pickupItems.Length > 0)
        {
            int randomIndex = Random.Range(0, pickupItems.Length);
            GameObject newPickup = Instantiate(pickupItems[randomIndex], SpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No pickups assigned to the spawner.");
        }
    }
}
