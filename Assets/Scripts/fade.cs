using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour
{

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Level"+LevelManager.GETLevel());
    }
    
    
}
