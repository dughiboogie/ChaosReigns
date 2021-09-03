using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : GlobalEffect
{
    public float speed = .01f;
    //public float startMovementCounter = 10f;

    private bool isActive = false;

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

    private void Update()
    {
        if(isActive) {
            Vector3 newScale = new Vector3(transform.localScale.x + speed, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        /*
        else {
            startMovementCounter -= Time.deltaTime;
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerManager>().Die();
        }
    }
}
