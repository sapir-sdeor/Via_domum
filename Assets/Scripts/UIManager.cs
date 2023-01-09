using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  
    private int _powerCounterPlayer1 = -1;
    private int _startCounterPlayer1 = -1;
    private int _powerCounterPlayer2 = -1;
    private int _startCounterPlayer2=-1;
    public static string PLAYER1 = "Player1";
    public static string PLAYER2 = "Player2";
    
    private int _indexPowerPlayer1=0, _indexPowerPlayer2=0;
    private float _indexHor1,_indexHor2;
    private LevelManager _levelManager;
    private bool _flyAlready;
    private  GameObject[] buttonManager1 = new GameObject[5], buttonManager2=new GameObject[5];
    private  GameObject[] buttonManagerAll1 = new GameObject[5], buttonManagerAll2=new GameObject[5];
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    private GameManager gameManager;
    private GameObject[] power1,power2,power3;

    private PlayerMovement.UIActions UImanager;
    private static readonly int Collect = Animator.StringToHash("collect");
    [SerializeField] private Sprite[] _sprites;

    private readonly float showNewPowerTime = 2f;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    


    public void ApplyPowerPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_powerCounterPlayer1 < 0)
            {
                return;
            }
            if (buttonManager1[_indexPowerPlayer1].CompareTag("fly"))
            {
                gameManager.GETPlayer2().Act(buttonManager1[_indexPowerPlayer1],
                buttonManager1[_indexPowerPlayer1].GetComponent<AudioSource>().clip);
            }
            else
            {
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

     public void NewNavigate(InputAction.CallbackContext context)
    {
       print(context.ReadValue<Vector2>().x);
    }
    
    public void CollectPowerPlayer(GameObject player,Collision2D power)
    {
        if (player.name== PLAYER1)
        {
           CollectPowerPlayer1(power);
        }
        else if (player.name == PLAYER2)
        {
            CollectPowerPlayer2(power);
        }
    }

    private void CollectPowerPlayer1(Collision2D power)
    {
        buttonManager1[++_powerCounterPlayer1] = new GameObject();
        DontDestroyOnLoad(buttonManager1[_powerCounterPlayer1]);
        buttonManager1[_powerCounterPlayer1].gameObject.tag = power.gameObject.tag;
        buttonManager1[_powerCounterPlayer1].gameObject.AddComponent<AudioSource>();
        buttonManager1[_powerCounterPlayer1].GetComponent<AudioSource>().clip = 
            power.gameObject.GetComponent<AudioSource>().clip;
        buttonManager1[_powerCounterPlayer1].GetComponent<AudioSource>().playOnAwake = false;
        _indexHor1 = _indexPowerPlayer1 = _powerCounterPlayer1;
        ShowNewPower(power.transform);
        StartCoroutine(ChangeButtonSprite1());
    }

    private void CollectPowerPlayer2(Collision2D power)
    {
        buttonManager2[++_powerCounterPlayer2] = new GameObject();
        DontDestroyOnLoad(buttonManager2[_powerCounterPlayer2]);
        buttonManager2[_powerCounterPlayer2].gameObject.tag = power.gameObject.tag;
        buttonManager2[_powerCounterPlayer2].gameObject.AddComponent<AudioSource>();
        buttonManager2[_powerCounterPlayer2].GetComponent<AudioSource>().clip = 
            power.gameObject.GetComponent<AudioSource>().clip;
        buttonManager2[_powerCounterPlayer2].GetComponent<AudioSource>().playOnAwake = false;
        ShowNewPower(power.transform);
        StartCoroutine(ChangeButtonSprite2());
    }

    public void NavigateMenu1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var val = context.ReadValue<Vector2>().x;
            if (_powerCounterPlayer1 < 0) return;
            if (_indexHor1 >= _powerCounterPlayer1 && val > 0) _indexHor1=0;
            else if (_indexHor1 <= 0 && val < 0) ;
            else { _indexHor1 += val;}
            foreach (var t in _sprites)
            {
                var spriteName = t.name;
                if (!buttonManager1[(int) _indexHor1].CompareTag(spriteName)) continue;
                _button1.GetComponent<Image>().sprite = t; 
                break;
            }
        }
        _indexPowerPlayer1 =(int) _indexHor1;
    }

    public void NavigateMenu2(InputAction.CallbackContext context)
    {
        print("navigateMenu2");
        if (_powerCounterPlayer2 < 0) return;
        if (context.performed)
        {
            var val = context.ReadValue<Vector2>().x;
            print(val + " val");
            if (_indexHor2 >= _powerCounterPlayer2 && val > 0) _indexHor2=0;
            else if(_indexHor2 <= 0 && val < 0) _indexHor2 = 0;
            else { _indexHor2 += val;}
            foreach (var t in _sprites)
            {
                var spriteName = t.name;
                if (!buttonManager2[(int) _indexHor2].CompareTag(spriteName)) continue;
                _button2.GetComponent<Image>().sprite = t; 
                break;
            }
            print(_indexHor2);
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

    private IEnumerator ChangeButtonSprite1()
    {
        yield return new WaitForSeconds(showNewPowerTime);
        for (int j = 0; j <_sprites.Length; j++)
        {
            String spriteName = _sprites[j].name;
            if (buttonManager1[_powerCounterPlayer1].CompareTag(spriteName))
            {
                _button1.GetComponent<Image>().sprite = _sprites[j];
                _button1.GetComponent<Image>().color = Color.white;
                break;
            }
        }
    }

    private IEnumerator ChangeButtonSprite2()
    {
        yield return new WaitForSeconds(showNewPowerTime);
        for (int j = 0; j <_sprites.Length; j++)
        {
            String spriteName = _sprites[j].name;
            if (buttonManager2[_powerCounterPlayer2].CompareTag(spriteName))
            {
                _button2.GetComponent<Image>().sprite = _sprites[j];
                _button2.GetComponent<Image>().color = Color.white;
                break;
            }
        }
        
    }
    
     IEnumerator SetOffMessage(Transform gameObjects)
    {
        yield return new WaitForSeconds(showNewPowerTime);
        if (gameObjects)
        {
            foreach (Transform newPower in gameObjects)
            {
                Destroy(newPower.gameObject);
            }
            Destroy(gameObjects.gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        for (int i = 0; i < buttonManager1.Length; i++)
        {
            buttonManager1[i] = buttonManagerAll1[i];
            buttonManager2[i] = buttonManagerAll2[i];
        }
        _powerCounterPlayer1 = _startCounterPlayer1;
        _powerCounterPlayer2 = _startCounterPlayer2;
        _indexHor1 = 0;
        _indexHor2 = 0;
        _indexPowerPlayer1 = 0;
        _indexPowerPlayer2 = 0;
    }

    public void SaveBeforeLoad()
    {
        _startCounterPlayer1 = _powerCounterPlayer1;
        _startCounterPlayer2 = _powerCounterPlayer2;
        print(_startCounterPlayer1 +" start counter player");
        for (int i = 0; i < buttonManager1.Length; i++)
        {
            buttonManagerAll1[i] = buttonManager1[i];
            buttonManagerAll2[i] = buttonManager2[i];
        }
        
    }
    
}
