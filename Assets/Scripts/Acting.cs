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
    [SerializeField] private Vector3 flyPosition;
    [SerializeField] private Light2D[] _light2D;
    [SerializeField] private Acting otherPlayer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int playerNumber;
    private static readonly Vector3 ScaleYoung = new(0.953071415f,0.716398299f,1f);
    private bool _onButton;
    private bool _onDiamond;
    private Rigidbody2D _rigidbody;
    private float _horizontal;
    private bool _isFacingRight;
    private PlayerMovement _inputAction;
 
    private void Start()
    {
        print(ButtonManger.Younger);
        if (ButtonManger.Younger == playerNumber) gameObject.transform.localScale = ScaleYoung;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpHeight);
    } 
    
    
    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var tran = transform;
        Vector3 localScale = tran.localScale;
        localScale.x *= -1f;
        tran.localScale = localScale;
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    void Update()
    {
        _rigidbody.velocity = new Vector2(_horizontal * speed, _rigidbody.velocity.y);
        if (!_isFacingRight && _horizontal > 0f) Flip();
        else if (_isFacingRight && _horizontal < 0f) Flip();
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("button"))
            ClickButton();
        else if (other.gameObject.CompareTag("stone"))
            CollectStone(other);
        else if (other.gameObject.name == "act")
            Act(other);
        else if (other.gameObject.CompareTag("light"))
            Act(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall"))
            _rigidbody.velocity = Vector2.zero;
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


    private void Act(Collision2D other)
    {
        //TODO: need to check if the act is active - press on the right key 
        Destroy(other.gameObject);
        MechanicFactory mechanicFactory = gameObject.AddComponent<MechanicFactory>();
        ICoreMechanic coreMechanic = mechanicFactory.CreateMechanic(other.gameObject.tag,
            _collider, flyPosition, sprite, background, _light2D);
        coreMechanic.ApplyMechanic();
    }

    private void CollectStone(Collision2D other)
    {
        UIManager.CollectPowerPlayer(gameObject.name);
        Destroy(other.gameObject);
    }

    private void ClickButton()
    {
        _onButton = true;
        if (otherPlayer.getOnButton())
            gameManager.OpenGate();
    }
    
    private void OnDiamond()
    {
        _onDiamond = true;
        if (otherPlayer.getOnDiamond())
            levelManager.LoadNextLevel();
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
