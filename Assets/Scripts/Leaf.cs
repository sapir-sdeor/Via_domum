using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Finish") )
        {
            print("out");
            GetComponentInChildren<Acting>().OutOfLeaf();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Finish") )
        {
            print("collision");
            GetComponentInChildren<Acting>().OutOfLeaf();
        }
    }
}
