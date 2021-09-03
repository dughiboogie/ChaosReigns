using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Earthquake : GlobalEffect
{
    private CinemachineImpulseSource impulseSource;
    private bool isActive;

    private void Start()
    {
        impulseSource = GameManager.instance.player.gameObject.GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if(isActive) {
            impulseSource.GenerateImpulse();
        }
    }

    public override void Apply()
    {
        base.Apply();
        isActive = true;
    }

    public override void Stop()
    {
        base.Stop();
        isActive = false;
    }
}
