using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreMechanic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Acting : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpHeight = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject bubbleFly;
    [SerializeField] private Vector3 flyPosition;
    [SerializeField] private Light2D[] _light2D;
    [SerializeField] private Acting otherPlayer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int playerNumber;
    [SerializeField] private UIManager uiManager;
    private static readonly Vector3 ScaleYoung = new(0.953071415f,0.716398299f,1f);
    private bool _onButton;
    private bool _onDiamond;
    private Rigidbody2D _rigidbody;
    private float _horizontal;
    private bool _isFacingRight;
    private PlayerMovement _inputAction;
    private Animator _animator;
 
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (ButtonManger.Younger == playerNumber) gameObject.transform.localScale = ScaleYoung;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public int GETPlayerNumber()
    {
        return playerNumber;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (GetComponent<Fly>() && GetComponent<Fly>().GETFly())
            return;
        if (context.performed && IsGrounded())
        {
            if (gameObject.name == "Player1")
            {
                if (!uiManager || !uiManager.getUIOpen1())
                {
                    _animator.SetTrigger("jump");
                    _animator.SetBool("wait", false);
                    StartCoroutine(WaitSecondForJump());
                }
            }
            if (gameObject.name == "Player2")
            {
                if (!uiManager || !uiManager.getUIOpen2())
                {
                    _animator.SetTrigger("jump");
                    _animator.SetBool("wait", false);
                    StartCoroutine(WaitSecondForJump());
                }
            }
        }
        
    }

    IEnumerator WaitSecondForJump()
    {
        yield return new WaitForSeconds(0.1f);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpHeight);
    }
    
    
    public void Move(InputAction.CallbackContext context)
    {
       
        if (gameObject.name == "Player1")
        {
            if (!uiManager || !uiManager.getUIOpen1())
            {
                _animator.SetBool("wait", false);
                _animator.SetBool("walk", true);
                _horizontal = context.ReadValue<Vector2>().x;
            }
        }

        if (gameObject.name == "Player2")
        {
            if (!uiManager || !uiManager.getUIOpen2())
            {
                _animator.SetBool("wait", false);
                _animator.SetBool("walk", true);
                _horizontal = context.ReadValue<Vector2>().x;
            }
        }
        
       
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var tran = transform;
        Vector3 localScale = tran.localScale;
        localScale.x *= -1f;
        tran.localScale = localScale;

        if (tran.childCount > 2)
        {
            var vector3 = tran.GetChild(2).localScale;
            vector3.x *= -1f;
            tran.GetChild(2).localScale = vector3;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)
               || gameManager.JumpEachOther();
    }


    void Update()
    {
        _rigidbody.velocity = new Vector2(_horizontal * speed, _rigidbody.velocity.y);
        if (!_isFacingRight && _horizontal > 0f) Flip();
        else if (_isFacingRight && _horizontal < 0f) Flip();
        if (_horizontal == 0)
            _animator.SetBool("walk", false);
        if (gameManager.JumpEachOther())
        {
            //TODO: play animation
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("button"))
            ClickButton();
        else if (other.gameObject.CompareTag("stone"))
            CollectStone(other);
        else if (other.gameObject.name == "act")
        {
            Destroy(other.gameObject);
            Act(other.gameObject);
        }
            
        else if (other.gameObject.CompareTag("light"))
        {
            Destroy(other.gameObject);
            Act(other.gameObject);
        }
            
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")){
          //  FindObjectOfType<Camera>().GetComponent<Animator>().SetTrigger("move");
            _rigidbody.velocity = Vector2.zero; 
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("button"))
            ClickButton();
        else if (other.gameObject.CompareTag("diamond"))
            OnDiamond();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("button"))
            _onButton = false;
        else if (other.gameObject.CompareTag("diamond"))
            _onDiamond = false;
    }


    public void Act(GameObject other)
    {
        MechanicFactory mechanicFactory = gameObject.AddComponent<MechanicFactory>();
        ICoreMechanic coreMechanic = mechanicFactory.CreateMechanic(other.gameObject.tag,
            _collider, flyPosition, sprite, background, _light2D, bubbleFly);
        coreMechanic.ApplyMechanic();
    }

    private void CollectStone(Collision2D other)
    {
        uiManager.CollectPowerPlayer(gameObject ,other);
        Destroy(other.gameObject);
    }

    private void ClickButton()
    {
        _onButton = true;
        if (otherPlayer.getOnButton() || LevelManager.GETLevel() == 2)
            gameManager.OpenGate();
    }
    
    private void OnDiamond()
    {
        _onDiamond = true;
        if (otherPlayer.getOnDiamond())
            levelManager.LoadNextLevel();
        _animator.SetBool("wait", true);
    }

    private bool getOnButton()
    {
        return _onButton;
    }
    
    private bool getOnDiamond()
    {
        return _onDiamond;
    }
}
