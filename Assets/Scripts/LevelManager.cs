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
    private static bool setLevelPos2 = false;
    private LevelManager _levelManager;
    private Vector3 pos2 = new(-0.330000013f, -1.80999994f, 0.0417999998f);
    private Vector3 pos1 = new(1.44000006f, -1.69000006f, 0.0417999998f);
    [SerializeField] private UIManager canvasToNotDestroy;
    

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        canvasToNotDestroy = FindObjectOfType<UIManager>();
        print(_gameManager);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    
    public void LoadNextLevel()
    {
        _level++;
        if (_level != 1)
        {
            DontDestroyOnLoad(canvasToNotDestroy);
            lastCanvas = canvasToNotDestroy;
            canvasToNotDestroy.GetComponent<UIManager>().SaveBeforeLoad();
            DontDestroyOnLoad(_gameManager);
            DontDestroyOnLoad(_gameManager.GETPlayer1().gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer2().gameObject);
        }
        SceneManager.LoadScene("Level"+_level);
    }

    public void SetPosPlayer1(Vector3 pos)
    {
        _gameManager.GETPlayer1().gameObject.transform.position = pos;
    }
    
    public void SetPosPlayer2(Vector3 pos)
    {
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
}
