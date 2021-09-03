using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : Pickup
{
    public override void PickupInteraction()
    {
        GameManager.instance.hud.AddCollectible();
    }
}
