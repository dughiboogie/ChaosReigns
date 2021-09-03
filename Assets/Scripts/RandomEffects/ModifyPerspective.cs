using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ModifyPerspective : GlobalEffect
{
    public List<CinemachineVirtualCamera> virtualCameras;
    private int currentCameraIndex = 0;

    public override void Apply()
    {
        base.Apply();

        int newCameraIndex = Random.Range(0, virtualCameras.Count);

        if(newCameraIndex == currentCameraIndex) {
            this.Apply();
        }
        else {
            virtualCameras[currentCameraIndex].Priority = 10;
            virtualCameras[newCameraIndex].Priority = 11;
            currentCameraIndex = newCameraIndex;
        }
    }

    public override void Stop()
    {
        base.Stop();

        virtualCameras[currentCameraIndex].Priority = 10;

        currentCameraIndex = 0;
        virtualCameras[currentCameraIndex].Priority = 11;
    }
}
