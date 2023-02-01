using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour
{

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial1")
        {
            SceneManager.LoadScene("Tutorial2");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("End");
        }
        else SceneManager.LoadScene("Level"+LevelManager.GETLevel());
    }
    
    
}
