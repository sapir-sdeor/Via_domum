using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static bool _openFirstUI1 = true;
    private static bool _openFirstUI2 = true;
    private static bool _useFirstPower1 = true;
    private static bool _useFirstPower2 = true;
    private static int _powerCounterPlayer1 = -1;
    private static  int _powerCounterPlayer2;
    public static string PLAYER1 = "Player1";
    public static string PLAYER2 = "Player2";
    private static bool _uiOpen1, _uiOpen2;
    
    private int _indexPowerPlayer1, _indexPowerPlayer2;
    private int _indexHor1, _indexVer1,_indexHor2,_indexVer2;
    private LevelManager _levelManager;
    private bool _flyAlready;
    private Button[] buttonManager1, buttonManager2;
    private GameManager gameManager;
    private bool setFalse1 = true,setFalse2 = true;
    private GameObject[] power1,power2,power3;

    private PlayerMovement.UIActions UImanager;
    private static readonly int Collect = Animator.StringToHash("collect");

    private void Start()
    {
        buttonManager1 = transform.GetChild(0).gameObject.GetComponentsInChildren<Button>();
        buttonManager2 = transform.GetChild(1).gameObject.GetComponentsInChildren<Button>();
        _levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
        SetActiveUIobject(buttonManager1, false);
        SetActiveUIobject(buttonManager2,false);
    }

    

    public void CancelPlayer1(InputAction.CallbackContext context)
    {
        _uiOpen1 = !_uiOpen1;
        if (_uiOpen1)
        {
            _indexHor1 = 0;
            _indexVer1 = 0;
            buttonManager1[_indexHor1].image.color = Color.magenta;
        }
        
        SetActiveUIobject(buttonManager1,_uiOpen1);
        _levelManager.CloseUIMessage(PLAYER1);
        if (_useFirstPower1)
        {
            _levelManager.UsePower1();
            _useFirstPower1 = false;
        }
    }

    public void CancelPlayer2(InputAction.CallbackContext context)
    {
        _uiOpen2 = !_uiOpen2;
        if (_uiOpen2)
        {
            _indexHor2 = 0;
            _indexVer2 = 0;
        }
        SetActiveUIobject(buttonManager2,_uiOpen2);
        _levelManager.CloseUIMessage(PLAYER2);
        if (_useFirstPower2)
        {
            _levelManager.UsePower2();
            _useFirstPower2 = false;
        }
    }

    public void Click1(InputAction.CallbackContext context)
    {
        if(_powerCounterPlayer1 >= 1) gameManager.OpenGate();
    }
    public void Click2(InputAction.CallbackContext context)
    {
        if(_powerCounterPlayer2 >= 1) gameManager.OpenGate();
    }
    
    public void ApplyPowerPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_powerCounterPlayer1 < 0)
            {
                _levelManager.tryAnotherTimeMessage(PLAYER1);
                return;
            }
            // if the power is fly we need to fly to other player
            if (buttonManager1[_indexPowerPlayer1].gameObject.CompareTag("fly"))
            {
                if (_flyAlready)
                {
                    _levelManager.tryAnotherTimeMessage(PLAYER1);
                    return;
                }
                gameManager.GETPlayer2().Act(buttonManager1[_indexPowerPlayer1].gameObject);
                _flyAlready = true;
            }
            else gameManager.GETPlayer1().Act(buttonManager1[_indexPowerPlayer1].gameObject);
        }
       
    }
    
    public void ApplyPowerPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_powerCounterPlayer2 < 0)
            {
                _levelManager.tryAnotherTimeMessage(PLAYER2);
                return;
            }
            if (buttonManager2[_indexPowerPlayer2].gameObject.CompareTag("fly"))
            {
                if (_flyAlready)
                {
                    _levelManager.tryAnotherTimeMessage(PLAYER2);
                    return;
                }
                gameManager.GETPlayer1().Act(buttonManager2[_indexPowerPlayer2].gameObject);
                _flyAlready = true;
            } 
            else gameManager.GETPlayer2().Act(buttonManager2[_indexPowerPlayer2].gameObject);
        }
    }

    private static void SetActiveUIobject(Button[] buttonManager,bool active)
    {
        foreach (Button _button in buttonManager)
        {
            _button.gameObject.SetActive(active);
        }
    }

    public bool getUIOpen1()
    {
        return _uiOpen1;
    }
    
    public bool getUIOpen2()
    {
        return _uiOpen2;
    }

    private void Update()
    {
        // if (LevelManager.GETLevel() == 1&& setFalse1)
        // {
        //     var gameObjects = GameObject.FindGameObjectsWithTag("little ui");
        //     foreach (var newPower in gameObjects)
        //     {
        //         newPower.gameObject.SetActive(false);
        //     } 
        //     power1 = gameObjects;
        //     setFalse1 = false;
        // }
        // else if (LevelManager.GETLevel() == 2 && setFalse2)
        // {
        //     print("level 2 should work");
        //     var gameObjectsFly = GameObject.FindGameObjectsWithTag("fly ui");
        //     var gameObjectsBlow = GameObject.FindGameObjectsWithTag("blowUp ui");
        //     print(gameObjectsBlow.Length +" count blow");
        //     print(gameObjectsFly.Length +"count fly");
        //     
        //     foreach (var newPower in gameObjectsFly)
        //     {
        //         print(newPower.gameObject.name + " name fly");
        //         newPower.gameObject.SetActive(false);
        //     }
        //
        //     foreach (var newPower in gameObjectsBlow)
        //     {
        //         print(newPower.gameObject.name + " name blow up");
        //         newPower.gameObject.SetActive(false);
        //     } 
        //     power2 = gameObjectsFly;
        //     power3 = gameObjectsBlow;
        //     setFalse2 = false;
        // }
    }


    //when one of the players collect power - update the counter and make the power available
    public void CollectPowerPlayer(GameObject player,Collision2D power)
    {
        if (player.name== PLAYER1)
        {
            buttonManager1[_indexPowerPlayer1].gameObject.tag = power.gameObject.tag;
            if (_openFirstUI1)
            {
                _levelManager.OpenUIMessage(player);
                _openFirstUI1 = false;
            }
            _powerCounterPlayer1++;
            buttonManager1[_powerCounterPlayer1].interactable = true;
            ShowNewPower(power.transform);
        }
        else if (player.name == PLAYER2)
        {
            buttonManager2[_indexPowerPlayer2].gameObject.tag = power.gameObject.tag;
            if (_openFirstUI2)
            {
                _levelManager.OpenUIMessage(player);
                _openFirstUI2 = false;
            }
            _powerCounterPlayer2++;
            buttonManager2[_powerCounterPlayer2].interactable = true;
            ShowNewPower(power.transform);
        }
    }

    public void NavigateMenu1(InputAction.CallbackContext context)
    {
        print(context.ReadValue<Vector2>().x);
        print(_indexHor1+" hor "+ _powerCounterPlayer1+" player");
        if (context.ReadValue<Vector2>().x > 0 && _indexHor1 < _powerCounterPlayer1) _indexHor1++;
        else if (context.ReadValue<Vector2>().x < 0 && _indexHor1 >= 1) _indexHor1--;
        for (int i = 0; i <= _powerCounterPlayer1; i++)
        {
            if(i!= _indexHor1) buttonManager1[i].image.color = Color.white;
            else{buttonManager1[_indexHor1].image.color = Color.magenta;}
        }
    }

    public void NavigateMenu2(InputAction.CallbackContext context)
    {
        
    }

    private void ShowNewPower(Transform power)
    {
        foreach (Transform newPowerChild in power)
        {
            print(newPowerChild.gameObject +" new power tag");
            newPowerChild.gameObject.SetActive(true);
        }
        StartCoroutine(SetOffMessage(power));
        
        // GameObject[] power;
        // switch (powerName)
        // {
        //     case "little":
        //         power = power1;
        //         break;
        //     case "fly":
        //         power = power2;
        //         break;
        //     case "blowUp":
        //         power = power3;
        //         break;
        //     default:
        //         power = null;
        //         break;
        // }
    }
    static IEnumerator SetOffMessage(Transform gameObjects)
    {
        yield return new WaitForSeconds(2);
        if (gameObjects)
        {
            foreach (Transform newPower in gameObjects)
            {
                Destroy(newPower.gameObject);
            }
            Destroy(gameObjects.gameObject);
        }
    }
}
