using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShrink2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player2")
        {
            LevelManager.SetShrink2();
        }
    } 
}
