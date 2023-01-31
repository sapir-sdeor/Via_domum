using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanic;
using Unity.VisualScripting;
using UnityEngine;

public class touchAct : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject[] leafs;
    private int _indexLeaf;
    public bool gotRona;
    public bool alreadyGrow;
    private static readonly int Connect = Animator.StringToHash("connect");
    private static readonly int Explode = Animator.StringToHash("explode");

    

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
        GetComponentInParent<Animator>().SetTrigger("root");
        if (leafs.Length == 0) return;
        if (_indexLeaf == leafs.Length)
            _indexLeaf = 0;
        leafs[_indexLeaf].GetComponent<Animator>().SetTrigger("moveLeaf");
        _indexLeaf++;
    }

    private void ConnectBubbles()
    {
        if (!background) return;
        gotRona = true;
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
