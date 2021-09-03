using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowers : MonoBehaviour
{
    public Power currentPower;
    public HUD hud;

    private void Awake()
    {
        currentPower = new Power();
    }

    public void ChangePower(Power newPower)
    {
        currentPower = newPower;

        hud.DisplayPowerupInfo(newPower);
    }

}
