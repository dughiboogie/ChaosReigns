using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : Pickup
{
    public Power power; 

    public override void PickupInteraction()
    {
        GameManager.instance.playerPowers.ChangePower(power);
    }
}
