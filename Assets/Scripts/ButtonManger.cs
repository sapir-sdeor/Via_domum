using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManger : MonoBehaviour
{
    public static int Younger;
    private LevelManager _levelManager;
    [SerializeField] private RuntimeAnimatorController animatorBigPlayer;
    [SerializeField] private RuntimeAnimatorController animatorYoungPlayer;
    
    private readonly Vector3 _pos1Level2 = new(2.16000009f,-2.10665536f,0.0770537108f);
    private readonly Vector3 _pos2Level2 = new(-3.63643527f,1.41309333f,0.0770537108f);

    private readonly Vector3 _pos1Level3 = new(4.01999998f, -4.07000017f, 0.0770537108f);
    private readonly Vector3 _pos2Level3 = new(-4.80000019f, 1.70000005f, 0.0770537108f);
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
        switch (LevelManager.GETLevel())
        {
            case 1:
                Younger = 1;
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                _levelManager.SetPosPlayer1(_pos1Level2);
                _levelManager.SetPosPlayer2(_pos2Level2);
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                _levelManager.SetPosPlayer1(_pos1Level3);
                _levelManager.SetPosPlayer2(_pos2Level3);
                SceneManager.LoadScene("Level3");
                break;
        }

    }
    
    public void SelectPlayer2()
    {
        switch (LevelManager.GETLevel())
        {
            case 1:
                Younger = 2;
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                _levelManager.SetPosPlayer1(_pos2Level2);
                _levelManager.SetPosPlayer2(_pos1Level2);
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                _levelManager.SetPosPlayer1(_pos2Level3);
                _levelManager.SetPosPlayer2(_pos1Level3);
                SceneManager.LoadScene("Level3");
                break;
        }

    }
    
}
