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
    private static int _powerCounterPlayer1;
    private static  int _powerCounterPlayer2;
    public static string PLAYER1 = "Player1";
    public static string PLAYER2 = "Player2";
    private static bool _uiOpen1, _uiOpen2;
    private int _indexPowerPlayer1, _indexPowerPlayer2;
    private LevelManager _levelManager;
    private bool _flyAlready;
    [SerializeField] private Button[] buttonManager1, buttonManager2;
    [SerializeField] private GameManager gameManager;

    private PlayerMovement.UIActions UImanager;
    private static readonly int Collect = Animator.StringToHash("collect");

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
        SetActiveUIobject(buttonManager1, false);
        SetActiveUIobject(buttonManager2,false);
    }
    
     
    public void CancelPlayer1( InputAction.CallbackContext context)
    {
        _uiOpen1 = !_uiOpen1;
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
        print("apply power");
        if (_powerCounterPlayer1 < 1)
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
    
    public void ApplyPowerPlayer2(InputAction.CallbackContext context)
    {
        if (_powerCounterPlayer2 < 1)
        {
            _levelManager.tryAnotherTimeMessage(PLAYER2);
            return;
        }
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
        return _uiOpen1;
    }
    
    public bool getUIOpen2()
    {
        return _uiOpen2;
    } 


    //when one of the players collect power - update the counter and make the power available
    public void CollectPowerPlayer(GameObject player,Collision2D message)
    {
        if (player.name== PLAYER1)
        {
            if (_openFirstUI1)
            {
                _levelManager.OpenUIMessage(player);
                _openFirstUI1 = false;
            }
            buttonManager1[_powerCounterPlayer1].interactable = true;
            _powerCounterPlayer1++;
            ShowNewPower(player,message);
        }
        else if (player.name == PLAYER2)
        {
            if (_openFirstUI2)
            {
                _levelManager.OpenUIMessage(player);
                _openFirstUI2 = false;
            }
            buttonManager2[_powerCounterPlayer2].interactable = true;
            _powerCounterPlayer2++;
            ShowNewPower(player,message);
        }
    }

    private void ShowNewPower(GameObject player,Collision2D message)
    {
        var pos = player.transform.position;
        GameObject newPower = Instantiate(message.gameObject,new Vector3(pos.x,pos.y + 1f,0),
            Quaternion.identity,player.transform);
        newPower.GetComponent<Collider2D>().enabled = false;
        newPower.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        var childCount = player.transform.childCount;
        player.transform.GetChild(childCount-1).gameObject.GetComponent<Animator>().SetBool(Collect, true);
        StartCoroutine(SetOffMessage(player.transform.GetChild(childCount-1).gameObject));
    }
    static IEnumerator SetOffMessage(GameObject message)
    {
        yield return new WaitForSeconds(2);
        Destroy(message);
    }
}
