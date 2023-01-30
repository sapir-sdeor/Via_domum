using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    private float time = 0;
    private bool _onLeaf;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("flower"))
        {
            print("on leaf");
            _onLeaf = true;
            print(other.transform);
            GetComponentsInParent<Transform>()[1].parent = other.transform;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponentsInParent<Collider2D>()[1].enabled = false;
        }

        else if (other.gameObject.CompareTag("Finish"))
        {
            print("exit leaf");
            _onLeaf = false;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponentsInParent<Collider2D>()[1].enabled = true;
            GetComponentsInParent<Transform>()[1].parent = null;
            GetComponentsInParent<Transform>()[1].eulerAngles = Vector3.zero;
        }
        
    }
}
