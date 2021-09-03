using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertKeys : GlobalEffect
{
    public PlayerController2D playerController;

    public override void Apply()
    {
        base.Apply();
        playerController.invertKeys = true;
    }

    public override void Stop()
    {
        base.Stop();
        playerController.invertKeys = false;
    }

}
