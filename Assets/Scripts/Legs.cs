using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("flower"))
        {
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponentInParent<Collider2D>().enabled = false;
            GetComponentInParent<Transform>().parent = other.transform;
        }
    }
}
