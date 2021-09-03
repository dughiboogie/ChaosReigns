using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    public float upwardThrustForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerGroundCheck") {
            collision.gameObject.GetComponentInParent<PlayerController2D>().UpThrust(upwardThrustForce);
        }
    }
}
