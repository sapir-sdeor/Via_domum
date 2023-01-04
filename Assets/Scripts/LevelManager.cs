using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int _level = 1;
    private GameManager _gameManager;
    private GameObject _openUIInstantiate1, _openUIInstantiate2;
    private float _messagePos = 0.7f;
    private UIManager lastCanvas;
    private int lastIndex1, lastIndex2;
    private int lastPower1, lastPower2;
    public static int Younger;
    public static int PlayerNumberInDownTunnel = 1;
    private LevelManager _levelManager;
    
    private readonly Vector3 _pos1Level2 = new(2.16000009f,-2.10665536f,0.0770537108f);
    private readonly Vector3 _pos2Level2 = new(-3.63643527f,1.41309333f,0.0770537108f);

    private readonly Vector3 _pos1Level3 = new(4.11999989f,-3.81999993f,0.0770537108f);
    private readonly Vector3 _pos2Level3 = new(-4.80000019f, 1.70000005f, 0.0770537108f);

    [SerializeField] private UIManager canvasToNotDestroy;
    
    
    /*private readonly Vector3 _pos1Message = new(7.03000021f, -4.3499999f, 0);
    private readonly Vector3 _pos2Message = new(-6.86f, -4.3499999f, 0);*/
    /*[SerializeField] private GameObject openUImessage1,openUImessage2;
    [SerializeField] private GameObject usePowerMessage1, usePowerMessage2;
    [SerializeField] private GameObject usePowerAnotherTimeMessage1,usePowerAnotherTimeMessage2;*/
    

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        canvasToNotDestroy = FindObjectOfType<UIManager>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadNextLevel()
    {
        _level++;
        DontDestroyOnLoad(_gameManager.GETPlayer1().gameObject);
        DontDestroyOnLoad(_gameManager.GETPlayer2().gameObject);
        DontDestroyOnLoad(canvasToNotDestroy);
        DontDestroyOnLoad(_gameManager);
        lastCanvas = canvasToNotDestroy;
        _gameManager.GETPlayer1().gameObject.SetActive(false);
        _gameManager.GETPlayer2().gameObject.SetActive(false);
        SelectPlayer1();


    }
    
    private void SelectPlayer1()
    {
        SceneManager.LoadScene("Level"+_level);
        StartCoroutine(SetPlayerPos());
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

    private IEnumerator SetPlayerPos()
    {
        yield return new WaitForSeconds(1);
        switch (_level)
        {
               
            case 1:
                Younger = 1;
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SetPosPlayer1(_pos1Level2);
                SetPosPlayer2(_pos2Level2);
                break;
            case 3:
                SetPosPlayer1(_pos1Level3);
                SetPosPlayer2(_pos2Level3);
                PlayerNumberInDownTunnel = 1;
                SceneManager.LoadScene("Level3");
                break;
        }
       
    }
    
    /*
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
    */

    /*public void tryAnotherTimeMessage(String PlayerName)
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
    }*/
}
