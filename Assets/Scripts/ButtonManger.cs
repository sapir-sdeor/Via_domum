using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManger : MonoBehaviour
{
    public static int Younger;
    public void StartButton()
    {
        SceneManager.LoadScene("characterSelecter");
    }

    public void SelectPlayer1()
    {
        Younger = 1;
        SceneManager.LoadScene("Level1");
    }
    
    public void SelectPlayer2()
    {
        Younger = 2;
        SceneManager.LoadScene("Level1");
    }
    
}
