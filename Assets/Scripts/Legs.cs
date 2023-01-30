using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("flower"))
        {
            print("start");
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponentsInParent<Transform>()[1].parent = other.transform;
            GetComponentsInParent<Collider2D>()[1].enabled = false;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            print("finish");
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponentsInParent<Collider2D>()[1].enabled = true;
            GetComponentsInParent<Transform>()[1].parent = null;
        }
        
    }
    
}
