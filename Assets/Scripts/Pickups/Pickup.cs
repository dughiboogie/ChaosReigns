using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            PickupInteraction();
            Destroy(gameObject);
        }
    }

    public abstract void PickupInteraction();
}
