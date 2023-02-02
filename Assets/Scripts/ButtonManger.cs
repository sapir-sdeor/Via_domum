
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
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
       // Destroy(FindObjectOfType<EventSystem>());
        SceneManager.LoadScene("HomeScreen");
    }

    private void Update()
    {
        if (GameObject.FindWithTag("canvas"))
        {
            if (SceneManager.GetActiveScene().name == "HomeScreen")
            {
                Destroy(GameObject.FindWithTag("canvas"));
                FindObjectOfType<EventSystem>().gameObject.SetActive(false);
            }
            else GameObject.FindWithTag("canvas").SetActive(false);
          //  Destroy(FindObjectOfType<EventSystem>());
        }
    }
}
