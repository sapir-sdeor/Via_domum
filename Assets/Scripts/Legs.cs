using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    private float time = 0;
    private bool _onLeaf; 
    private bool pass;
    private Transform leaf;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("flower") && !_onLeaf && !pass)
        {
            _onLeaf = true;
            leaf = other.transform;
            GetComponentInParent<Animator>().SetBool("walk", false);
            GetComponentsInParent<Transform>()[1].parent = leaf;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponentsInParent<Collider2D>()[1].enabled = false;
        }

        else if (other.gameObject.CompareTag("Finish"))
        {
            _onLeaf = false;
            pass = true;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponentsInParent<Collider2D>()[1].enabled = true;
            GetComponentsInParent<Transform>()[1].parent = null;
            GetComponentsInParent<Transform>()[1].eulerAngles = Vector3.zero;
        }
    }

    private void Update()
    {
        if (_onLeaf)
        {
            GetComponentsInParent<Transform>()[1].parent = leaf;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponentsInParent<Collider2D>()[1].enabled = false;
        }
        else
        {
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponentsInParent<Collider2D>()[1].enabled = true;
            GetComponentsInParent<Transform>()[1].parent = null;
            GetComponentsInParent<Transform>()[1].eulerAngles = Vector3.zero;
        }
    }
}