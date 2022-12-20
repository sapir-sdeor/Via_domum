using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int _level = 2;
    private bool _tutorial;
    [SerializeField] private GameObject openUImessage1;
    [SerializeField] private GameObject openUImessage2;
    [SerializeField] private GameObject usePowerMessage1;
    [SerializeField] private GameObject usePowerMessage2;
    private void Start()
    {
        _tutorial = _level == 1;
    }

    public void LoadNextLevel()
    {
        _level++;
        SceneManager.LoadScene("Level" + _level);
    }

    public static int GETLevel()
    {
        return _level;
    }

    public void OpenUIMessagePlayer1()
    {
        if (_tutorial) openUImessage1.gameObject.SetActive(true);
    }
    
    public void OpenUIMessagePlayer2()
    {
        if (_tutorial) openUImessage2.gameObject.SetActive(true);
    }
    
    public void CloseUIMessagePlayer1()
    {
        if (_tutorial) openUImessage1.gameObject.SetActive(false);
    }
    
    public void CloseUIMessagePlayer2()
    {
        if (_tutorial) openUImessage2.gameObject.SetActive(false);
    }



}
