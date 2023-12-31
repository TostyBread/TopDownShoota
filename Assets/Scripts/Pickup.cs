using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    //public GameObject Weapon;

    public GameObject[] PickupFeedbacks;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        PickedUp(col);

        foreach (var feedback in PickupFeedbacks)
        {
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    protected virtual void PickedUp(Collider2D col)
    {

    }
}
