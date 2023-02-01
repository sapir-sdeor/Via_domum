using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkManager : MonoBehaviour
{
    [SerializeField] private GameObject shrinkLeft, shrinkRight;
    private static GameObject _shrinkLeft, _shrinkRight;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _shrinkLeft = shrinkLeft;
        _shrinkRight = shrinkRight;
    }

    public static void SetLeftShrink()
    {
        _shrinkLeft.SetActive(true);
    }

    public static void setRightShrink()
    {
        _shrinkRight.SetActive(true);
    }
}
