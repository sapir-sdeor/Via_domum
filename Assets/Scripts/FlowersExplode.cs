using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersExplode : MonoBehaviour
{

    private bool changeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        changeColor = !changeColor;
        if(changeColor) gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
