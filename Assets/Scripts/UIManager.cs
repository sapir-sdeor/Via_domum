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
    private static bool UIOpen1, UIOpen2;
    private static Button[] _buttonManager1, _buttonManager2;
    
    [SerializeField] private Button[] buttonManager1, buttonManager2;
     private PlayerMovement.UIActions UImanager;

     private void Start()
    {
        _buttonManager1 = buttonManager1;
        _buttonManager2 = buttonManager2;
        SetActiveUIobject(_buttonManager1, false);
        SetActiveUIobject(_buttonManager2,false);
    }
    

    public void CancelPlayer1( InputAction.CallbackContext context)
    {
        SetActiveUIobject(_buttonManager1,UIOpen1);
        UIOpen1 = UIOpen1 == false;
    }

    public void CancelPlayer2(InputAction.CallbackContext context)
    {
   
        SetActiveUIobject(_buttonManager2,UIOpen2);
        UIOpen2 = UIOpen2 == false;
    }

    public void Click(InputAction.CallbackContext context)
    {
        _buttonManager1[0].enabled = false;
    }


    private static void SetActiveUIobject(Button[] buttonManager,bool active)
    {
        foreach (Button _button in buttonManager )
        {
            _button.gameObject.SetActive(active);
        }
    }

    public bool getUIOpen1()
    {
        return UIOpen1;
    }
    
    public bool getUIOpen2()
    {
        return UIOpen2;
    } 


    //when one of the players collect power - update the counter and make the power available
    public static void CollectPowerPlayer(String name)
    {
        if (name== Player1)
        {
            SetActiveUIobject(_buttonManager1,true);
            _buttonManager1[powerCounterPlayer1].interactable = true;
            powerCounterPlayer1++;
            UIOpen1 = true;

        }
        else if (name == Player2)
        {
            SetActiveUIobject(_buttonManager2,true);
            _buttonManager2[powerCounterPlayer2].interactable = true;
            powerCounterPlayer2++;
            UIOpen2 = true;
        }
    }
}
