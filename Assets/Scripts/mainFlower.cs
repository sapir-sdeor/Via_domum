using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainFlower : MonoBehaviour
{
    private void Start()
    {
        PlayFlowersAnimation();
    }

    private void PlayFlowersAnimation()
    {
        foreach (var animator in GetComponentsInChildren<Animator>())
        {
            animator.SetTrigger("wind");
        }
    }
    
    
    
    
    
}
