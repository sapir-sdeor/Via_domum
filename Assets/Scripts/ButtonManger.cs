using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManger : MonoBehaviour
{
    public static int Younger;
    private LevelManager _levelManager;
    
    private Vector3 _nextPos1 = new(2.16000009f,-2.10665536f,0.0770537108f);
    private Vector3 _nextPos2 = new(-3.63643527f,1.41309333f,0.0770537108f);
    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("characterSelecter0");
    }

    public void SelectPlayer1()
    {
        if (LevelManager.GETLevel() == 1)
        {
            Younger = 1;
            SceneManager.LoadScene("Level1");
        }
        else if (LevelManager.GETLevel() == 2)
        {
            _levelManager.SetPosPlayer1(_nextPos1);
            _levelManager.SetPosPlayer2(_nextPos2);
            SceneManager.LoadScene("Level2");
        }
       
    }
    
    public void SelectPlayer2()
    {
        if (LevelManager.GETLevel() == 1)
        {
            Younger = 2;
            SceneManager.LoadScene("Level1");
        }
        else if (LevelManager.GETLevel() == 2)
        {
            _levelManager.SetPosPlayer1(_nextPos2);
            _levelManager.SetPosPlayer2(_nextPos1);
            SceneManager.LoadScene("Level2");
        }
        
    }
    
}
