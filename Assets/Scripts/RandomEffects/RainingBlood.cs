using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingBlood : GlobalEffect {

    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
    }

    public override void Apply()
    {
        base.Apply();

        particles.gameObject.SetActive(true);
        particles.Play();
    }

    public override void Stop()
    {
        base.Stop();

        particles.Stop();
    }

}
