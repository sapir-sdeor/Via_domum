using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int _level = 2;
    public void LoadNextLevel()
    {
        _level++;
        SceneManager.LoadScene("Level" + _level);
    }
    
    
}
