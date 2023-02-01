
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
}
