using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool _openFirstUI1 = true;
    private bool _openFirstUI2 = true;
    private bool _useFirstPower1 = true;
    private bool _useFirstPower2 = true;
    private int _powerCounterPlayer1 = -1;
    private int _powerCounterPlayer2 = -1;
    public static string PLAYER1 = "Player1";
    public static string PLAYER2 = "Player2";
    private bool _uiOpen1, _uiOpen2;
    
    private int _indexPowerPlayer1=0, _indexPowerPlayer2=0;
    private int _indexHor1,_indexHor2;
    private LevelManager _levelManager;
    private bool _flyAlready;
    private Button[] buttonManager1, buttonManager2;
    private GameManager gameManager;
    private bool setFalse1 = true,setFalse2 = true;
    private GameObject[] power1,power2,power3;

    private PlayerMovement.UIActions UImanager;
    private static readonly int Collect = Animator.StringToHash("collect");
    [SerializeField] private Sprite[] _spriteGlow,_sprites;

    private void Start()
    {
        _openFirstUI1 = true;
        _openFirstUI2 = true;
        _useFirstPower1 = true;
        _useFirstPower2 = true;
        _powerCounterPlayer1 = -1; _powerCounterPlayer2 = -1;
        _indexPowerPlayer1 = 0; _indexPowerPlayer2 = 0;
        _uiOpen1 = false;
        _uiOpen2 = false;
        buttonManager1 = transform.GetChild(0).gameObject.GetComponentsInChildren<Button>();
        buttonManager2 = transform.GetChild(1).gameObject.GetComponentsInChildren<Button>();
        _levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
        SetActiveUIobject(buttonManager1, false);
        SetActiveUIobject(buttonManager2, false);
    }
    

    public void CancelPlayer1(InputAction.CallbackContext context)
    {
        print("cancel player" + _indexPowerPlayer1);
        _uiOpen1 = !_uiOpen1;
        if (_uiOpen1)
        {
            _indexHor1 = 0;
         
            for (int j = 0; j <_spriteGlow.Length; j++)
            {
                String spriteName = _spriteGlow[j].name;
                if (buttonManager1[_indexPowerPlayer1].CompareTag(spriteName))
                {
                    buttonManager1[_indexPowerPlayer1].GetComponent<Image>().sprite = _spriteGlow[j]; 
                    break;
                }
            }
        }
        
        SetActiveUIobject(buttonManager1,_uiOpen1);
        _levelManager.CloseUIMessage(PLAYER1);
        if (_useFirstPower1)
        {
            _useFirstPower1 = false;
        }
    }

    public void CancelPlayer2(InputAction.CallbackContext context)
    {
        _uiOpen2 = !_uiOpen2;
        if (_uiOpen2)
        {
            _indexHor2 = 0;
         
            for (int j = 0; j <_spriteGlow.Length; j++)
            {
                String spriteName = _spriteGlow[j].name;
                if (buttonManager2[_indexPowerPlayer2].CompareTag(spriteName))
                {
                    buttonManager2[_indexPowerPlayer2].GetComponent<Image>().sprite = _spriteGlow[j]; 
                    break;
                }
            }
        }
        SetActiveUIobject(buttonManager2,_uiOpen2);
        _levelManager.CloseUIMessage(PLAYER2);
        if (_useFirstPower2)
        {
            //_levelManager.UsePower2();
            _useFirstPower2 = false;
        }
    }

    public void Click1(InputAction.CallbackContext context)
    {
        if(_powerCounterPlayer1 >= 1) gameManager.OpenGate();
    }
    public void Click2(InputAction.CallbackContext context)
    {
        if(_powerCounterPlayer2 >= 1) gameManager.OpenGate();
    }
    
    public void ApplyPowerPlayer1(InputAction.CallbackContext context)
    {
        print(_indexPowerPlayer1 + " apply player");
        if (context.performed)
        {
            if (_powerCounterPlayer1 < 0)
            {
               // _levelManager.tryAnotherTimeMessage(PLAYER1);
                return;
            }
            // if the power is fly we need to fly to other player
            if (buttonManager1[_indexPowerPlayer1].gameObject.CompareTag("fly"))
            {
                gameManager.GETPlayer2().Act(buttonManager1[_indexPowerPlayer1].gameObject,
                buttonManager1[_indexPowerPlayer1].GetComponent<AudioSource>().clip);
                //_flyAlready = true;
            }
            else
            {
                gameManager.GETPlayer1().Act(buttonManager1[_indexPowerPlayer1].gameObject,
                    buttonManager1[_indexPowerPlayer1].GetComponent<AudioSource>().clip);
            }
        }
       
    }
    
    public void ApplyPowerPlayer2(InputAction.CallbackContext context)
    {
        print(_indexPowerPlayer2 + " apply player");
        if (context.performed)
        {
            if (_powerCounterPlayer2 < 0)
            {
               // _levelManager.tryAnotherTimeMessage(PLAYER2);
                return;
            }
            if (buttonManager2[_indexPowerPlayer2].gameObject.CompareTag("fly"))
            {
                gameManager.GETPlayer1().Act(buttonManager2[_indexPowerPlayer2].gameObject,
                    buttonManager2[_indexPowerPlayer2].GetComponent<AudioSource>().clip);
            }
            else
            {
                buttonManager2[_indexPowerPlayer2].GetComponent<AudioSource>().Play();
                gameManager.GETPlayer2().Act(buttonManager2[_indexPowerPlayer2].gameObject,
                    buttonManager2[_indexPowerPlayer2].GetComponent<AudioSource>().clip);
            }
        }
    }

    public static void SetActiveUIobject(Button[] buttonManager,bool active)
    {
        foreach (Button _button in buttonManager)
        {
            _button.gameObject.SetActive(active);
        }
    }

    public bool getUIOpen1()
    {
        return _uiOpen1;
    }
    
    public bool getUIOpen2()
    {
        return _uiOpen2;
    }
    


    //when one of the players collect power - update the counter and make the power available
    public void CollectPowerPlayer(GameObject player,Collision2D power)
    {
        if (player.name== PLAYER1)
        {
            if (_openFirstUI1)
            {
               // _levelManager.OpenUIMessage(player);
                _openFirstUI1 = false;
            }
            _powerCounterPlayer1++;
            buttonManager1[_powerCounterPlayer1].gameObject.tag = power.gameObject.tag;
            buttonManager1[_powerCounterPlayer1].gameObject.AddComponent<AudioSource>();
            buttonManager1[_powerCounterPlayer1].GetComponent<AudioSource>().clip = 
                power.gameObject.GetComponent<AudioSource>().clip;
            buttonManager1[_powerCounterPlayer1].GetComponent<AudioSource>().playOnAwake = false;
            for (int j = 0; j <_sprites.Length; j++)
            {
                String spriteName = _sprites[j].name;
                if (buttonManager1[_powerCounterPlayer1].CompareTag(spriteName))
                {
                    buttonManager1[_powerCounterPlayer1].GetComponent<Image>().sprite = _sprites[j];
                    buttonManager1[_powerCounterPlayer1].GetComponent<Image>().color = Color.white;
                    break;
                }
            }
            buttonManager1[_powerCounterPlayer1].interactable = true;
            ShowNewPower(power.transform);
        }
        else if (player.name == PLAYER2)
        {
            if (_openFirstUI2)
            {
               // _levelManager.OpenUIMessage(player);
                _openFirstUI2 = false;
            }
            _powerCounterPlayer2++;
            buttonManager2[_powerCounterPlayer2].gameObject.tag = power.gameObject.tag;
            buttonManager2[_powerCounterPlayer2].gameObject.AddComponent<AudioSource>();
            buttonManager2[_powerCounterPlayer2].GetComponent<AudioSource>().clip = 
                power.gameObject.GetComponent<AudioSource>().clip;
            buttonManager2[_powerCounterPlayer2].GetComponent<AudioSource>().playOnAwake = false;
            for (int j = 0; j <_sprites.Length; j++)
            {
                String spriteName = _sprites[j].name;
                if (buttonManager2[_powerCounterPlayer2].CompareTag(spriteName))
                {
                    buttonManager2[_powerCounterPlayer2].GetComponent<Image>().sprite = _sprites[j];
                    buttonManager2[_powerCounterPlayer2].GetComponent<Image>().color = Color.white;
                    break;
                }
            }
            buttonManager2[_powerCounterPlayer2].interactable = true;
            ShowNewPower(power.transform);
        }
    }

    public void NavigateMenu1(InputAction.CallbackContext context)
    {
        print(_indexHor1 +" index hor");
        if (context.ReadValue<Vector2>().x > 0 && _indexHor1 < _powerCounterPlayer1) _indexHor1++;
        else if (context.ReadValue<Vector2>().x < 0 && _indexHor1 >= 1) _indexHor1--;
        for (int i = 0; i <= _powerCounterPlayer1; i++)
        {
            if (i != _indexHor1)
            {
                for (int j = 0; j <_sprites.Length; j++)
                {
                    String spriteName = _sprites[j].name;
                    if (buttonManager1[_indexHor1].CompareTag(spriteName))
                    {
                        buttonManager1[_indexHor1].GetComponent<Image>().sprite = _sprites[j]; 
                        break;
                    }
                }
            }
            else
            {
                for (int j = 0; j <_spriteGlow.Length; j++)
                {
                    String spriteName = _spriteGlow[j].name;
                    if (buttonManager1[_indexHor1].CompareTag(spriteName))
                    {
                        buttonManager1[_indexHor1].GetComponent<Image>().sprite = _spriteGlow[j]; 
                        break;
                    }
                }
            }
        }

        _indexPowerPlayer1 = _indexHor1;
    }

    public void NavigateMenu2(InputAction.CallbackContext context)
    {
        print(_indexHor2 +" index hor");
        if (context.ReadValue<Vector2>().x > 0 && _indexHor2 < _powerCounterPlayer2) _indexHor2++;
        else if (context.ReadValue<Vector2>().x < 0 && _indexHor2 >= 1) _indexHor2--;
        for (int i = 0; i <= _powerCounterPlayer2; i++)
        {
            if (i != _indexHor2)
            {
                for (int j = 0; j <_sprites.Length; j++)
                {
                    String spriteName = _sprites[j].name;
                    if (buttonManager2[_indexHor2].CompareTag(spriteName))
                    {
                        buttonManager2[_indexHor2].GetComponent<Image>().sprite = _sprites[j]; 
                        break;
                    }
                }
            }
            else
            {
                for (int j = 0; j <_spriteGlow.Length; j++)
                {
                    String spriteName = _spriteGlow[j].name;
                    if (buttonManager2[_indexHor2].CompareTag(spriteName))
                    {
                        buttonManager2[_indexHor2].GetComponent<Image>().sprite = _spriteGlow[j]; 
                        break;
                    }
                }
            }
        }
        _indexPowerPlayer2 = _indexHor2;
    }

    private void ShowNewPower(Transform power)
    {
        foreach (Transform newPowerChild in power)
        {
            newPowerChild.gameObject.SetActive(true);
        }
        power.gameObject.GetComponent<Collider2D>().enabled = false;
        power.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        StartCoroutine(SetOffMessage(power));
    }
    
    static IEnumerator SetOffMessage(Transform gameObjects)
    {
        yield return new WaitForSeconds(2);
        if (gameObjects)
        {
            foreach (Transform newPower in gameObjects)
            {
                Destroy(newPower.gameObject);
            }
            Destroy(gameObjects.gameObject);
        }
    }

    /*public void InitializedToLastCanvas1(int spritesToRemove)
    {
        for (int j = 0; j < spritesToRemove; j++)
        {
            buttonManager1[_indexPowerPlayer1-j].GetComponent<Image>().sprite = null; 
        }
    }
    
    public void InitializedToLastCanvas2(int spritesToRemove)
    {
        for (int j = 0; j < spritesToRemove; j++)
        {
            buttonManager2[_indexPowerPlayer2-j].GetComponent<Image>().sprite = null; 
        }
    }
    
    public int GETPowerCounter1()
    {
        return _powerCounterPlayer1;
    }
    public int GETPowerCounter2()
    {
        return _powerCounterPlayer2;
    }
    
    public int GETIndexPower1()
    {
        return _indexPowerPlayer1;
    }
    public int GETIndexPower2()
    {
        return _indexPowerPlayer2;
    }
    
    public void SetPowerCounter1(int val)
    {
        _powerCounterPlayer1 = val;
    }
    public void SetPowerCounter2(int val)
    {
        _powerCounterPlayer2 = val;
    }
    
    public void SetIndexPower1(int val)
    {
        _indexPowerPlayer1 = val;
    }
    public void SetIndexPower2(int val)
    {
        _indexPowerPlayer2 = val;
    }*/
}
