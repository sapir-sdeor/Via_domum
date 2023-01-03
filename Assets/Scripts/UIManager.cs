using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  
    private int _powerCounterPlayer1 = -1;
    private int _powerCounterPlayer2 = -1;
    public static string PLAYER1 = "Player1";
    public static string PLAYER2 = "Player2";
    
    private int _indexPowerPlayer1=0, _indexPowerPlayer2=0;
    private float _indexHor1,_indexHor2;
    private LevelManager _levelManager;
    private bool _flyAlready;
    private GameObject[] buttonManager1 = new GameObject[5], buttonManager2=new GameObject[5];
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    private GameManager gameManager;
    private GameObject[] power1,power2,power3;

    private PlayerMovement.UIActions UImanager;
    private static readonly int Collect = Animator.StringToHash("collect");
    [SerializeField] private Sprite[] _spriteGlow,_sprites;

    private void Start()
    {
       
        // _powerCounterPlayer1 = -1; _powerCounterPlayer2 = -1;
        // _indexPowerPlayer1 = 0; _indexPowerPlayer2 = 0;
        // buttonManager1 = transform.GetChild(0).gameObject.GetComponentsInChildren<Button>();
        // buttonManager2 = transform.GetChild(1).gameObject.GetComponentsInChildren<Button>();
        _levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
        // SetActiveUIobject(buttonManager1, false);
        // SetActiveUIobject(buttonManager2, false);
    }
    


    public void ApplyPowerPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_powerCounterPlayer1 < 0)
            {
                return;
            }
            // if the power is fly we need to fly to other player
            if (buttonManager1[_indexPowerPlayer1].CompareTag("fly"))
            {
                gameManager.GETPlayer2().Act(buttonManager1[_indexPowerPlayer1],
                buttonManager1[_indexPowerPlayer1].GetComponent<AudioSource>().clip);
                //_flyAlready = true;
            }
            else
            {
                print("should act");
                gameManager.GETPlayer1().Act(buttonManager1[_indexPowerPlayer1],
                    buttonManager1[_indexPowerPlayer1].GetComponent<AudioSource>().clip);
            }
        }
       
    }
    
    public void ApplyPowerPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_powerCounterPlayer2 < 0)
            {
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

  

    


    //when one of the players collect power - update the counter and make the power available
    public void CollectPowerPlayer(GameObject player,Collision2D power)
    {
        if (player.name== PLAYER1)
        {
            buttonManager1[++_powerCounterPlayer1] = new GameObject();
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
                    _button1.GetComponent<Image>().sprite = _sprites[j];
                    // buttonManager1[_powerCounterPlayer1].GetComponent<Image>().color = Color.white;
                    break;
                }
            }
            _indexHor1 = _indexPowerPlayer1 = _powerCounterPlayer1;
            ShowNewPower(power.transform);
        }
        else if (player.name == PLAYER2)
        {
         
            buttonManager2[++_powerCounterPlayer2] = new GameObject();
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
                    _button2.GetComponent<Image>().sprite = _sprites[j];
                    break;
                }
            }
            ShowNewPower(power.transform);
        }
    }

    public void NavigateMenu1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_indexHor1 < _powerCounterPlayer1) _indexHor1+= 1;
            else _indexHor1 = 0;
            print(_indexHor1+" index hor 1");
            for (int i = 0; i < _sprites.Length; i++)
            {
                String spriteName = _sprites[i].name;
                if (buttonManager1[(int)_indexHor1].CompareTag(spriteName))
                {
                    _button1.GetComponent<Image>().sprite = _sprites[i]; 
                    break;
                }
            }
        }
        _indexPowerPlayer1 =(int) _indexHor1;
    }

    public void NavigateMenu2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_indexHor2 < _powerCounterPlayer2) _indexHor2+= 1;
            else _indexHor2 = 0;
            print(_indexHor2+" index hor 2");
            for (int i = 0; i < _sprites.Length; i++)
            {
                String spriteName = _sprites[i].name;
                if (buttonManager2[(int)_indexHor2].CompareTag(spriteName))
                {
                    _button2.GetComponent<Image>().sprite = _sprites[i]; 
                    break;
                }
            }
        }
        _indexPowerPlayer2 =(int) _indexHor2;
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
    
    // public static void SetActiveUIobject(Button[] buttonManager,bool active)
    // {
    //     foreach (Button _button1 in buttonManager)
    //     {
    //         _button1.gameObject.SetActive(active);
    //     }
    // }

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
