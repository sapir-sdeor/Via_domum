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
    // private float _messagePos = 0.7f;
    private int lastIndex1, lastIndex2;
    private int lastPower1, lastPower2;
    private static bool setLevelPos2 = false;
    private LevelManager _levelManager;
    private float time;
    private bool applyHint;
    [SerializeField] private Vector3 holePosition;
    [SerializeField] private Vector3 runaPosition;
    [SerializeField] private Vector3 mushroomPosition;
    [SerializeField] private float timeForHint;
    [SerializeField] private float timeHintAppear = 6f;
    [SerializeField] private UIManager canvasToNotDestroy;
    [SerializeField] private GameObject hint;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeForHint && !applyHint)
        {
            switch (_level)
            {
                case 1:
                    CheckHintsLevel1();
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
    }

    private void CheckHintsLevel1()
    {
        if (FindObjectOfType<UIManager>()._powerCounterPlayer1 == 0 
                && FindObjectOfType<UIManager>()._powerCounterPlayer2 == 0 )
        {
            print("littlehint");
            hint.transform.position = GameObject.FindWithTag("little").transform.position;
            hint.SetActive(true);
            applyHint = true;
        }
        else if (!_gameManager.GETPlayer1().gotHole &&
                 !_gameManager.GETPlayer2().gotHole)
        {
            print("holehint");
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



    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        canvasToNotDestroy = FindObjectOfType<UIManager>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadNextLevel()
    {
        _level++;
        if (_level != 1)
        {
            DontDestroyOnLoad(canvasToNotDestroy);
            canvasToNotDestroy.GetComponent<UIManager>().SaveBeforeLoad();
            DontDestroyOnLoad(_gameManager.gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer1().gameObject);
            DontDestroyOnLoad(_gameManager.GETPlayer2().gameObject);
            DontDestroyOnLoad(FindObjectOfType<EventSystem>());
            
        }
        GameObject.FindGameObjectWithTag("fade").GetComponent<Animator>().SetTrigger("fadeOut");
    }

  

    public static int GETLevel()
    {
        return _level;
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
