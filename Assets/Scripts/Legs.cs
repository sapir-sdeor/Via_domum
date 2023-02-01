using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    private bool _onLeaf; 
    public static bool Pass;
    private Transform leaf;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("flower") && !_onLeaf && !Pass)
        {
            _onLeaf = true;
            leaf = other.transform;
            GetComponentInParent<SpriteRenderer>().sortingOrder = 9;
            GetComponentInParent<Animator>().SetBool("walk", false);
            GetComponentsInParent<Transform>()[1].parent = leaf;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponentsInParent<Collider2D>()[1].enabled = false;
        }

        else if (other.gameObject.CompareTag("Finish"))
        {
            _onLeaf = false;
            Pass = true;
            GetComponentInParent<SpriteRenderer>().sortingOrder = 4;
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
