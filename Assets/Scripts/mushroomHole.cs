using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class mushroomHole : MonoBehaviour
{
    private bool _grow;
    private GameManager _gameManager;
    private bool _move;
    private float _time;

    public bool Grow
    {
        get => _grow;
        set => _grow = value;
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        GameObject mushroom = GameObject.FindWithTag("mushroom");
        if (GetComponentInChildren<Collider2D>().IsTouching
            (_gameManager.GETPlayer1().GetComponent<Collider2D>()))
        {
            GetComponent<Animator>().SetBool("goInside", true);
            mushroom.GetComponent<Animator>().SetBool("return", true);
            mushroom.GetComponent<touchAct>().alreadyGrow = false;
        }
        else if (GetComponentInChildren<Collider2D>().IsTouching
            (_gameManager.GETPlayer2().GetComponent<Collider2D>()))
        {
            GetComponent<Animator>().SetBool("goInside", true);
            mushroom.GetComponent<Animator>().SetBool("return",true);
            mushroom.GetComponent<touchAct>().alreadyGrow = false;
        }
        else
        {
            GetComponent<Animator>().SetBool("goInside", false);
            mushroom.GetComponent<Animator>().SetBool("return",false);
            _grow = true;
        }
    }
    
}
