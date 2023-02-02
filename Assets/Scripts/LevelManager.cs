using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Touch = UnityEngine.Touch;

public class LevelManager : MonoBehaviour
{

    private static int _level = -1;
    private GameManager _gameManager;
    private GameObject _openUIInstantiate1, _openUIInstantiate2;
    private int lastIndex1, lastIndex2;
    private int lastPower1, lastPower2;
    private LevelManager _levelManager;
    private float time;
    private bool applyHint,applyHintLeft,applyHintRight;
    [SerializeField] private Vector3 holePosition;
    [SerializeField] private Vector3 runaPosition;
    [SerializeField] private Vector3 mushroomPosition;
    [SerializeField] private Vector3 littlePosition;
    
    private Vector3 echoPosition = new(-1.59000003f,2.27999997f,0);
    private Vector3 rootPosition = new(1.16999996f,0.0299999993f,0);
    private Vector3 flyPosition = new(2.6400001f,-2.82999992f,0);

   
    private Vector3 tunnelPos2 = new Vector3(-1.51999998f,2.97000003f,0.0153808258f);
    private Vector3 tunnelPos1= new Vector3(0.109999999f,-1.46000004f,0f);
    // private Vector3 UIposLeft =new Vector3(-8.97999954f, -4.61000013f, 0.0153808258f);
    // private Vector3 UIposRight =new Vector3(6.05000019f,-4.61000013f,0.0153808258f);
    // private Vector3 UIscale = new(2, 2, 0);
    private static bool passTunnelPos2, passTunnelPos1;
    private static bool shrink1,shrink2, echo;
    
    [SerializeField] private float timeForHint;
    [SerializeField] private float timeHintAppear = 6f;
    [SerializeField] private UIManager canvasToNotDestroy;
    [SerializeField] private GameObject hint;
        //,hintUILeft,hintUIRight;
        

    public static void SetPassTunnelPos2()
    {
        passTunnelPos2 = true;
    }
    
    public static void SetPassTunnelPos1()
    {
        passTunnelPos1 = true;
    }
    
    public static void SetShrink1()
    {
        shrink1 = true;
    }
    public static void SetShrink2()
    {
        shrink2 = true;
    }
    public static void SetEcho()
    {
        echo = true;
    }
    private void Update()
    {
        if (_level < 1)
        {
            return;
        }
        time += Time.deltaTime;
        if (time > timeForHint && !applyHint)
        {
            switch (_level)
            {
                case 1:
                    CheckHintsLevel1();
                    break;
                case 2:
                    CheckHintsLevel2();
                    break;
                case 3:
                    CheckHintsLevel3();
                    break;
            }
            time = 0;
        }

        if (applyHint && time > timeHintAppear)
        {
            hint.SetActive(false);
            time = 0;
            applyHint = false;
        }
        if (applyHintLeft && time > timeHintAppear)
        {
            hint.SetActive(false);
            time = 0;
            applyHintLeft = false;
        }
        
        if (applyHintRight && time > timeHintAppear)
        {
            hint.SetActive(false);
            time = 0;
            applyHintRight = false;
        }
    }

    private void CheckHintsLevel1()
    {
        if (FindObjectOfType<UIManager>()._powerCounterPlayer1 == 0 
                && FindObjectOfType<UIManager>()._powerCounterPlayer2 == 0 )
        {
            hint.transform.position = littlePosition;
            hint.SetActive(true);
            applyHint = true;
        }
        else if (!_gameManager.GETPlayer1().gotHole &&
                 !_gameManager.GETPlayer2().gotHole)
        {
            hint.transform.position = holePosition;
            hint.SetActive(true);
            applyHint = true;
        }
        else
        {
            touchAct[] touches = FindObjectsOfType<touchAct>();
            if (!touches[0].gotRona && !touches[1].gotRona)
            {
                hint.transform.position = runaPosition;
                hint.SetActive(true);
                applyHint = true;
            }
            else
            {
                GameObject mushroomHole = GameObject.FindGameObjectWithTag("mushroomHole");
                if (!mushroomHole.GetComponent<mushroomHole>().enabled ||
                    !mushroomHole.GetComponent<mushroomHole>().Grow)
                {
                    hint.transform.position = mushroomPosition;
                    hint.SetActive(true);
                    applyHint = true;
                }
            }
        }
        
    }

    private void CheckHintsLevel2()
    {
        GameObject echo = GameObject.FindWithTag("echo");
        if (echo && echo.name == "stone")
        {
            hint.transform.position = echoPosition;
            hint.SetActive(true);
            applyHint = true;
        }
        else if (!Legs.Pass)
        {
            hint.transform.position = rootPosition;
            hint.SetActive(true);
            applyHint = true;
        }
        else if (!Echo.removeWebs)
        {
            hint.transform.position = flyPosition;
            hint.SetActive(true);
            applyHint = true;
        }
    }
    
    private void CheckHintsLevel3()
    { 
        if (!passTunnelPos2)
        {
            hint.transform.position = tunnelPos2;
            hint.SetActive(true);
            applyHint = true;
            print("tunnelPos2");
        }
        else if (!passTunnelPos1)
        {
            hint.transform.position = tunnelPos1;
            hint.SetActive(true);
            applyHint = true;
            print("tunnelPos1");
        }
      
        /*else if (!shrink1)
        {
            // hint.transform.position = UIposRight;
            // hint.transform.localScale = UIscale;
            hintUIRight.SetActive(true);
            applyHintRight = true;
            print("shrink1");
        }
        else if (!echo||!shrink2)
        {
            // hint.transform.position = UIposLeft;
            // hint.transform.localScale = UIscale;
            hintUILeft.SetActive(true);
            applyHintLeft = true;
            print("shrink2");
        }*/
    }


    private void Awake()
    {
        _level = -1;
        _gameManager = FindObjectOfType<GameManager>();
        canvasToNotDestroy = FindObjectOfType<UIManager>();
        print(_gameManager);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadNextLevel()
    {
        _level++;
        print(_level);
        if (_level == 0)
        {
            DontDestroyOnLoad(canvasToNotDestroy);
            canvasToNotDestroy.GetComponent<UIManager>().SaveBeforeLoad();
            DontDestroyOnLoad(_gameManager.gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer1().gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer2().gameObject);
            if (FindObjectsOfType<EventSystem>().Length > 1)
            {
                DontDestroyOnLoad(FindObjectsOfType<EventSystem>()[1]);
                FindObjectsOfType<EventSystem>()[0].gameObject.SetActive(false);
            }
            else
            {
                DontDestroyOnLoad(FindObjectsOfType<EventSystem>()[0]);
            }
        }
        else if (_level == 4)
        {
            Destroy(_gameManager.gameObject);
            Destroy(_gameManager.GETPlayer1().gameObject);
            Destroy(_gameManager.GETPlayer2().gameObject);
        }
        GameObject.FindGameObjectWithTag("fade").GetComponent<Animator>().SetTrigger("fadeOut");
    }

  

    public static int GETLevel()
    {
        return _level;
    }
    
    public static void SetLevel(int level)
    {
        _level = level;
    }
    
    public void CloseUIMessage(String playerName)
    {
        if (playerName == UIManager.PLAYER1)
        {
            if (!_openUIInstantiate1) return;
            _openUIInstantiate1.SetActive(false);
        }
        else if(playerName == UIManager.PLAYER2)
        {
            if (!_openUIInstantiate2) return;
            _openUIInstantiate2.SetActive(false); 
        }
    }
}
