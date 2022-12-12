using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static int powerCounterPlayer1;
    private static  int powerCounterPlayer2;
    private static string Player1 = "Player1";
    private static string Player2 = "Player2";
    private static bool UIOpen1, UIOpen2;
    private static Button[] _buttonManager1, _buttonManager2;
    private int _indexPowerPlayer1, _indexPowerPlayer2;
    
    [SerializeField] private Button[] buttonManager1, buttonManager2;
    [SerializeField] private GameManager gameManager;
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
        UIOpen1 = !UIOpen1;
        SetActiveUIobject(_buttonManager1,UIOpen1);
        print("cancel player 2" + UIOpen2);
    }

    public void CancelPlayer2(InputAction.CallbackContext context)
    {
        UIOpen2 = !UIOpen2;
        SetActiveUIobject(_buttonManager2,UIOpen2);
       
        print("cancel player 2" + UIOpen2);
    }

    public void Click1(InputAction.CallbackContext context)
    {
        if(powerCounterPlayer1 >= 1) gameManager.OpenGate();
    }
    public void Click2(InputAction.CallbackContext context)
    {
        print(powerCounterPlayer2);
        if(powerCounterPlayer2 >= 1) gameManager.OpenGate();
    }
    
    public void ApplyPowerPlayer1(InputAction.CallbackContext context)
    {
        // if the power is fly we need to fly to other player
        if (powerCounterPlayer1 < 1) return;
        if (buttonManager1[_indexPowerPlayer1].gameObject.CompareTag("fly"))
            gameManager.GETPlayer2().Act(buttonManager1[_indexPowerPlayer1].gameObject);
        else gameManager.GETPlayer1().Act(buttonManager1[_indexPowerPlayer1].gameObject);
    }
    
    public void ApplyPowerPlayer2(InputAction.CallbackContext context)
    {
        if (powerCounterPlayer2 < 1) return;
        gameManager.GETPlayer2().Act(buttonManager2[_indexPowerPlayer2].gameObject);
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
            // UIOpen1 = true;
            SetActiveUIobject(_buttonManager1,UIOpen1);
            _buttonManager1[powerCounterPlayer1].interactable = true;
            powerCounterPlayer1++;
        }
        else if (name == Player2)
        {
            // UIOpen2 = true;
            SetActiveUIobject(_buttonManager2,UIOpen2);
            _buttonManager2[powerCounterPlayer2].interactable = true;
            powerCounterPlayer2++;
           
        }
    }
}
