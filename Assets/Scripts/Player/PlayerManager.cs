using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Player died");

        // Play death animation

        GameManager.instance.ReloadLevel();
    }


}
