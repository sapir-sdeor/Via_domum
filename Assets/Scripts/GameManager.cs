using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject diamond;
    private GameObject player1;
    private GameObject player2;

    private void Start()
    {
        Acting[] players = FindObjectsOfType<Acting>();
        if (players[0].GETPlayerNumber() == 1) {
            player1 = players[0].gameObject;
            player2 = players[1].gameObject; 
        }
        else {
            player1 = players[1].gameObject;
            player2 = players[0].gameObject;
        }
    }

    public void OpenGate()
    {
        gate.GetComponent<Animator>().SetTrigger("open");
        gate.GetComponent<Collider2D>().enabled = false;
    }
    
    public void FallDiamond()
    {
        diamond.GetComponent<Rigidbody2D>().gravityScale = 1;
        StartCoroutine(DisableDiamond());

    }
    
    IEnumerator DisableDiamond()
    {
        yield return new WaitForSeconds(1f);
        diamond.GetComponent<Rigidbody2D>().gravityScale = 0;
        diamond.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        diamond.GetComponent<Collider2D>().isTrigger = true;

    }

    public bool JumpEachOther()
    {
        Collider2D[] colliderPlayer1 = player1.GetComponentsInChildren<Collider2D>();
        Collider2D[] colliderPlayer2 = player2.GetComponentsInChildren<Collider2D>();
        if (colliderPlayer1.Length != 3 && colliderPlayer2.Length != 3) return false;
        return colliderPlayer1[1].IsTouching(colliderPlayer2[2]) || colliderPlayer1[2].IsTouching(colliderPlayer2[1]);
    }

    public Acting GETPlayer1()
    {
        return player1.GetComponent<Acting>();
    }
    
    public Acting GETPlayer2()
    {
        return player2.GetComponent<Acting>();
    }
}
