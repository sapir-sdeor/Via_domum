using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEcho : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player1")
        {
            LevelManager.SetEcho();
        }
    }
}
