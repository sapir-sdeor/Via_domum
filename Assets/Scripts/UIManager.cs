using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static int powerCounterPlayer1=0;
    private static  int powerCounterPlayer2 = 0;
    [SerializeField] private Button[] buttonManager1, buttonManager2;
    private static Button[] _buttonManager1, _buttonManager2;

    private void Start()
    {
        _buttonManager1 = buttonManager1;
        _buttonManager2 = buttonManager2;
    }


    //when one of the players collect power - update the counter and make the power available
    public static void CollectPowerPlayer(String name)
    {
        if (name== "Player1")
        {
            print(name);
            _buttonManager1[powerCounterPlayer1].interactable = true;
            powerCounterPlayer1++;

        }
        else if (name == "Player2")
        {
            _buttonManager2[powerCounterPlayer2].interactable = true;
            powerCounterPlayer2++;
        }
    }
}
