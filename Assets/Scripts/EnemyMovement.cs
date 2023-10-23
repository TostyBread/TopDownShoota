using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{

    public Transform Target;

    protected override void HandleInput() //bot input here
    {
        if (Target == null)
            Target = GameObject.FindWithTag("Player").transform;

        if (Target == null)
            return;

        _inputDirection =
            (Target.position - transform.position).normalized; //player position - initial bot position (basically trying to minimize the distance between player and bot)
    }

}
