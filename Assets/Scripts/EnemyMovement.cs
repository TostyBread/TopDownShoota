using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{

    public Transform currentTarget;

    
    protected override void HandleInput() //bot input here
    {

        GameObject player = GameObject.FindWithTag("Player"); // Declare player tag to GameObject player

        GameObject[] deployedItems = GameObject.FindGameObjectsWithTag("CatFood"); // Declare catfood tag to GameObject array (if multiple catfood deployed)

        if (deployedItems.Length > 0) // if catfood is deployed, currenttarget will be switching to deploy item
        {
            currentTarget = FindClosestTarget(deployedItems);
        }
        else if (player != null) // if player exist, chases player instead
        {
            currentTarget = player.transform;
        }
        Transform FindClosestTarget(GameObject[] targets) // Distaces of the closest target
        {
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (var target in targets) // checking multiple catfood
            {
                float distance = Vector2.Distance(transform.position, target.transform.position); // assign distance with target's length

                if (distance < closestDistance) // if one of the enemies has the closest distance to catfood, they will chase that first
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                }
            }
            return closestTarget;
        }

        if (player == null) // if player is not available, don't do anything
            return;

        // input direction will be assign with current target position - initial bot position (basically trying to minimize the distance between target and bot)

        _inputDirection =
            (currentTarget.position - transform.position).normalized;
    }

}
