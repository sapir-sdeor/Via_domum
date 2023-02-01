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

    /*public void ApplyMove()
    {
        Acting.DontMove = false;
        Acting[] players = FindObjectsOfType<Acting>();
        players[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        players[1].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }*/
    
    
}
