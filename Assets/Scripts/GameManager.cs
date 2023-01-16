
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject diamond;
    [SerializeField] private RuntimeAnimatorController animatorBigPlayer;
    [SerializeField] private RuntimeAnimatorController animatorYoungPlayer;
    private GameObject _player1;
    private GameObject _player2;

    private void Awake()
    {
        Acting[] players = FindObjectsOfType<Acting>();
        diamond = GameObject.FindGameObjectWithTag("diamond");
        if (players.Length < 2) return; 
        if (players[0].GETPlayerNumber() == 1) {
            _player1 = players[0].gameObject;
            _player2 = players[1].gameObject; 
        }
        else {
            _player1 = players[1].gameObject;
            _player2 = players[0].gameObject;
        }
    }

    public void OpenGate()
    {
        gate.GetComponent<Animator>().SetTrigger("open");
        gate.GetComponent<Collider2D>().enabled = false;
    }
    
    public void FallDiamond(GameObject stone)
    {
        diamond = GameObject.FindGameObjectWithTag("diamond");
        if (!stone) stone = diamond;
        stone.GetComponent<Rigidbody2D>().gravityScale = 1;
        StartCoroutine(DisableDiamond(stone));
    }
    
    IEnumerator DisableDiamond(GameObject stone)
    {
        yield return new WaitForSeconds(1.5f);
        if (stone == diamond)
        {
            stone.GetComponent<Rigidbody2D>().gravityScale = 0;
            stone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            stone.GetComponent<Collider2D>().isTrigger = true;
        }
        else if (stone && stone.GetComponent<Rigidbody2D>()) Destroy(stone.GetComponent<Rigidbody2D>());
    }

    public int JumpEachOtherWhoUp()
    {
        Collider2D[] colliderPlayer1 = _player1.GetComponentsInChildren<Collider2D>();
        Collider2D[] colliderPlayer2 = _player2.GetComponentsInChildren<Collider2D>();
        if (colliderPlayer1.Length < 3 || colliderPlayer2.Length < 3) return 0;
        if (colliderPlayer1[1].IsTouching(colliderPlayer2[2])) return 1;
        return colliderPlayer1[2].IsTouching(colliderPlayer2[1]) ? 2 : 0;
    }

    public Acting GETPlayer1()
    {
        return _player1.GetComponent<Acting>();
    }
    
    public Acting GETPlayer2()
    {
        return _player2.GetComponent<Acting>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Acting[] players = FindObjectsOfType<Acting>();
        diamond = GameObject.FindGameObjectWithTag("diamond");
        if (players.Length < 2) return; 
        if (players[0].GETPlayerNumber() == 1) {
            _player1 = players[0].gameObject;
            _player2 = players[1].gameObject; 
        }
        else {
            _player1 = players[1].gameObject;
            _player2 = players[0].gameObject;
        }
    }
}
