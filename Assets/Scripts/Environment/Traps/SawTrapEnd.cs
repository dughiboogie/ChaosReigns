using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrapEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap") {
            collision.GetComponent<SawTrapMovement>().ChangeDirection();
        }
    }
}
