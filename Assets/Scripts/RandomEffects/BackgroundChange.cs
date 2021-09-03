using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : GlobalEffect
{
    public new Camera camera;

    Color originalColor = new Color(0.260591f, 0.2766505f, 0.3018868f, 1f);
    private bool isActive = false;
    float timeLeft;
    Color targetColor;

    public override void Apply()
    {
        base.Apply();
        isActive = true;
    }

    public override void Stop()
    {
        base.Stop();
        isActive = false;

        camera.backgroundColor = originalColor;
    }

    void Update()
    {
        if(isActive) {
            if(timeLeft <= Time.deltaTime) {
                // transition complete
                // assign the target color
                camera.backgroundColor = targetColor;

                // start a new transition
                targetColor = new Color(Random.value, Random.value, Random.value);
                timeLeft = 1.0f;
            }
            else {
                // transition in progress
                // calculate interpolated color
                camera.backgroundColor = Color.Lerp(camera.backgroundColor, targetColor, Time.deltaTime / timeLeft);

                // update the timer
                timeLeft -= Time.deltaTime;
            }
        }        
    }
}
