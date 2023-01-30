using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int _level = 2;
    private GameManager _gameManager;
    private GameObject _openUIInstantiate1, _openUIInstantiate2;
    // private float _messagePos = 0.7f;
    private int lastIndex1, lastIndex2;
    private int lastPower1, lastPower2;
    private static bool setLevelPos2 = false;
    private LevelManager _levelManager;
    [SerializeField] private UIManager canvasToNotDestroy;
    

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
        if (_level != 1)
        {
            DontDestroyOnLoad(canvasToNotDestroy);
            canvasToNotDestroy.GetComponent<UIManager>().SaveBeforeLoad();
            DontDestroyOnLoad(_gameManager.gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer1().gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer2().gameObject);
        }
        GameObject.FindGameObjectWithTag("fade").GetComponent<Animator>().SetTrigger("fadeOut");
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
