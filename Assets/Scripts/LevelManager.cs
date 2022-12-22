using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    private static int _level = 2;
    private GameManager _gameManager;
    private GameObject _openUIInstantiate1;
    private GameObject _openUIInstantiate2;
    private readonly Vector3 _pos1 = new(7.03000021f, -4.3499999f, 0);
    private readonly Vector3 _pos2 = new(-6.86f, -4.3499999f, 0);
    [SerializeField] private GameObject openUImessage1;
    [SerializeField] private GameObject openUImessage2;
    [SerializeField] private GameObject usePowerMessage1;
    [SerializeField] private GameObject usePowerMessage2;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadNextLevel()
    {
        _level++;
        SceneManager.LoadScene("middlleLevel");
    }

    public static int GETLevel()
    {
        return _level;
    }

    public void OpenUIMessagePlayer1()
    {
        if (!openUImessage1) return;
        Acting player1 = _gameManager.GETPlayer1();
        Vector3 pos = player1.transform.position;
        _openUIInstantiate1 = Instantiate(openUImessage1.gameObject,
            new Vector3(pos.x+ 0.7f,pos.y,0),Quaternion.identity,player1.transform);
    }

    public void OpenUIMessagePlayer2()
    {
        if (!openUImessage2) return;
        Acting player2 = _gameManager.GETPlayer2();
        Vector3 pos = player2.transform.position;
        _openUIInstantiate2 = Instantiate(openUImessage2.gameObject,
            new Vector3(pos.x + 0.7f, pos.y, 0), Quaternion.identity, player2.transform);
    }

    public void CloseUIMessagePlayer1()
    {
        if (!_openUIInstantiate1) return;
        _openUIInstantiate1.SetActive(false);
    }
    
    public void CloseUIMessagePlayer2()
    {
        if (!_openUIInstantiate2) return;
        _openUIInstantiate2.SetActive(false);
    }

    public void UsePower1()
    {
        if (!usePowerMessage1) return;
        Instantiate(usePowerMessage1.gameObject, _pos1, Quaternion.identity);    
    }
    
    public void UsePower2()
    {
        if (!usePowerMessage2) return;
        Instantiate(usePowerMessage2.gameObject, _pos2, Quaternion.identity);    
    }
    
}
