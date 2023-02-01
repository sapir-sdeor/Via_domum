
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonManger : MonoBehaviour
{
    
    
    public void StartGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Open GIF");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial1");
    }

    public void HomeScreen(InputAction.CallbackContext context)
    {
        Destroy(GameObject.FindWithTag("canvas"));
        SceneManager.LoadScene("HomeScreen");
    }

    private void Update()
    {
        if (GameObject.FindWithTag("canvas"))
            GameObject.FindWithTag("canvas").SetActive(false);
    }
}
