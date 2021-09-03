using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffect : MonoBehaviour
{
    public new string name;

    public virtual void Apply()
    {
        Debug.Log("Apply " + name + " effect");
    }

    public virtual void Stop()
    {
        Debug.Log("Stop " + name + " effect");
    }
}
