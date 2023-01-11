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
    private Vector3 _target = new(-3.1500001f, 6.51000023f, 0f);
    private bool _move;
    [SerializeField] private Sprite stayInHoleSprite;
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
        if (_grow)
        {
            GetComponentInChildren<Collider2D>().enabled = true;
        }
        else return;
        if (GetComponentInChildren<Collider2D>().IsTouching
            (_gameManager.GETPlayer1().GetComponent<Collider2D>()))
        {
            print("here");
            GetComponent<Animator>().SetBool("goInside", true);
        }
        else if (GetComponentInChildren<Collider2D>().IsTouching
            (_gameManager.GETPlayer2().GetComponent<Collider2D>()))
        {
            print("here1");
            GetComponent<Animator>().SetBool("goInside", true);
        }
        else  GetComponent<Animator>().SetBool("goInside", false);
    }
    
}
