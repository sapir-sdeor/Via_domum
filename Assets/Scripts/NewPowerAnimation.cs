using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPowerAnimation : MonoBehaviour
{
    [SerializeField] private Animator newPowerAnimator;
    private static readonly int Player1 = Animator.StringToHash("player1");
    private static readonly int Player2 = Animator.StringToHash("player2");


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == UIManager.PLAYER1)
        {
            newPowerAnimator.SetBool(Player1, true);
        }
        else if (other.gameObject.name == UIManager.PLAYER2)
        {
            newPowerAnimator.SetBool(Player2, true);
        }
    }
}
