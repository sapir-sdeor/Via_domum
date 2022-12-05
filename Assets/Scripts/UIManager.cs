using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static int powerCounterPlayer1=0;
    private static  int powerCounterPlayer2 = 0;
    private static string Player1 = "Player1";
    private static string Player2 = "Player2";
    private static Button[] _buttonManager1, _buttonManager2;
    
    [SerializeField] private Button[] buttonManager1, buttonManager2;
     private PlayerMovement.UIActions UImanager;

    private void Start()
    {
        _buttonManager1 = buttonManager1;
        _buttonManager2 = buttonManager2;
    }
    
    private void Update()
    {
        // if (Input.GetKey(KeyCode.K))
        // {
        //     SetActiveUIobject(_buttonManager1,false);
        // }
        // if(Input.GetKey(KeyCode.G))
        // {
        //     SetActiveUIobject(_buttonManager2,false);
        // }
    }

    public void View( InputAction.CallbackContext context)
    {
       
    }


    private static void SetActiveUIobject(Button[] buttonManager,bool active)
    {
        foreach (Button _button in buttonManager )
        {
            _button.gameObject.SetActive(active);
        }
    }


    //when one of the players collect power - update the counter and make the power available
    public static void CollectPowerPlayer(String name)
    {
        if (name== Player1)
        {
            SetActiveUIobject(_buttonManager1,true);
            _buttonManager1[powerCounterPlayer1].interactable = true;
            powerCounterPlayer1++;

        }
        else if (name == Player2)
        {
            SetActiveUIobject(_buttonManager2,true);
            _buttonManager2[powerCounterPlayer2].interactable = true;
            powerCounterPlayer2++;
        }
    }
}
