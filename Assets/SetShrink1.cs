using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShrink1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player1")
        {
            LevelManager.SetShrink1();
        }
    }
}
