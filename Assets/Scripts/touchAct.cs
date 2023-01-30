using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanic;
using Unity.VisualScripting;
using UnityEngine;

public class touchAct : MonoBehaviour
{
    [SerializeField] private GameObject background;
    private GameObject[] _flower;
    private int _indexFlower;
    public bool alreadyGrow;
    private static readonly int Connect = Animator.StringToHash("connect");
    private static readonly int Explode = Animator.StringToHash("explode");

    private void Start()
    {
        _flower = GameObject.FindGameObjectsWithTag("flower");
    }

    public void TouchFactory()
    {
        switch (tag)
        {
            case "connect":
                ConnectBubbles();
                break;
            case "mushroom":
                GrowMushroom();
                break;
            case "root":
                ApplyRoot();
                break;
        }
    }

    private void ApplyRoot()
    {
        print("touchRoot");
        if (_indexFlower < _flower.Length)
        {
            _flower[_indexFlower].GetComponent<Animator>().SetBool(Explode, true);
            if (_flower[_indexFlower].name == "Main Flower")
            {
                foreach (var animator in _flower[_indexFlower].GetComponentsInChildren<Animator>())
                    animator.SetTrigger("wind");
            }
        }
        _indexFlower++;
    }

    private void ConnectBubbles()
    {
        if (!background) return;
        background.GetComponent<Collider2D>().isTrigger = true;
        background.GetComponent<Animator>().SetTrigger(Connect);
    }

    private void GrowMushroom()
    {
        if (alreadyGrow) return;
        GetComponent<Animator>().SetTrigger("goInside");
        GameObject mushroomHole = GameObject.FindGameObjectWithTag("mushroomHole");
        if (!mushroomHole.GetComponent<mushroomHole>().enabled)
            mushroomHole.GetComponent<mushroomHole>().enabled = true;
        mushroomHole.GetComponent<Animator>().SetTrigger("grow");
        mushroomHole.GetComponent<mushroomHole>().Grow = true;
        alreadyGrow = true;
    }
}
