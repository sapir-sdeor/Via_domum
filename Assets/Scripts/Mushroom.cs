using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static Animator animator;

    private void Start()
    {
        animator = _animator;
    }

    public static void MushroomAnimation()
    {
        animator.SetTrigger("Collision");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        animator.SetTrigger("Collision");
    }
}
