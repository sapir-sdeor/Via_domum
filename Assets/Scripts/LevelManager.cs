using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    private static int _level = 1;
    private GameManager _gameManager;
    private GameObject _openUIInstantiate1, _openUIInstantiate2;
    private readonly Vector3 _pos1Message = new(7.03000021f, -4.3499999f, 0);
    private readonly Vector3 _pos2Message = new(-6.86f, -4.3499999f, 0);
    private float _messagePos = 0.7f;
   
    [SerializeField] private GameObject canvasToNotDestroy;
    [SerializeField] private GameObject openUImessage1,openUImessage2;
    [SerializeField] private GameObject usePowerMessage1, usePowerMessage2;
    [SerializeField] private GameObject usePowerAnotherTimeMessage1,usePowerAnotherTimeMessage2;
    

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    
    public void LoadNextLevel()
    {
        _level++;
        DontDestroyOnLoad(_gameManager.GETPlayer1().gameObject);
        DontDestroyOnLoad(_gameManager.GETPlayer2().gameObject);
        DontDestroyOnLoad(canvasToNotDestroy);
        DontDestroyOnLoad(_gameManager);
        _gameManager.GETPlayer1().gameObject.SetActive(false);
        _gameManager.GETPlayer2().gameObject.SetActive(false);
        SceneManager.LoadScene("characterSelecter" + _level);
    }

    public void SetPosPlayer1(Vector3 pos)
    {
        _gameManager.GETPlayer1().gameObject.SetActive(true);
        _gameManager.GETPlayer1().gameObject.transform.position = pos;
    }
    
    public void SetPosPlayer2(Vector3 pos)
    {
        _gameManager.GETPlayer2().gameObject.SetActive(true);
        _gameManager.GETPlayer2().gameObject.transform.position = pos;
    }
    
    
    public static int GETLevel()
    {
        return _level;
    }
    
    public void CloseUIMessage(String playerName)
    {
        if (playerName == UIManager.PLAYER1)
        {
            if (!_openUIInstantiate1) return;
            _openUIInstantiate1.SetActive(false);
        }
        else if(playerName == UIManager.PLAYER2)
        {
            if (!_openUIInstantiate2) return;
            _openUIInstantiate2.SetActive(false); 
        }
    }
    
    public void OpenUIMessage(GameObject player)
    {
        Vector3 pos = player.transform.position;
        var trans = player.transform;
        if (player.name == UIManager.PLAYER1)
        {
            if (!openUImessage1) return;
            _openUIInstantiate1 = Instantiate(openUImessage1.gameObject,
                new Vector3(pos.x+ _messagePos,pos.y,0),Quaternion.identity,trans);
        }
        else if (player.name == UIManager.PLAYER2)
        {
            if (!openUImessage2) return;
            _openUIInstantiate2 = Instantiate(openUImessage2.gameObject,
                new Vector3(pos.x + _messagePos, pos.y, 0), Quaternion.identity,trans);
        }
    }

    public void UsePower1()
    {
        if (!usePowerMessage1) return;
        Instantiate(usePowerMessage1.gameObject, _pos1Message, Quaternion.identity);    
    }
    
    public void UsePower2()
    {
        if (!usePowerMessage2) return;
        Instantiate(usePowerMessage2.gameObject, _pos2Message, Quaternion.identity);    
    }

    public void tryAnotherTimeMessage(String PlayerName)
    {
        if (PlayerName == UIManager.PLAYER1)
        {
            var player = _gameManager.GETPlayer1();
            Vector3 pos = player.transform.position;
            var trans = player.transform;
            Instantiate(usePowerAnotherTimeMessage1.gameObject, pos + Vector3.right
                , Quaternion.identity);
        }
        else if (PlayerName == UIManager.PLAYER2)
        {
            var player = _gameManager.GETPlayer2();
            Vector3 pos = player.transform.position;
            var trans = player.transform;
            Instantiate(usePowerAnotherTimeMessage2.gameObject, pos + Vector3.right,
                Quaternion.identity);
        }
    }
}
