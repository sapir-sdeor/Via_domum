using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish") && GetComponentInChildren<Acting>())
        {
            GetComponentInChildren<Acting>().OutOfLeaf();
        }
    }
}
