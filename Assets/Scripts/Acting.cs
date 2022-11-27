using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acting : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    private PlayerMovement _playerActions;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private Vector2 _jumpInput;

    private void Awake()
    {
        _playerActions = new PlayerMovement();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerActions.Player_Action.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Action.Disable();
    }

    void Update()
    {
        _moveInput = _playerActions.Player_Action.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = _moveInput * _speed;

        if (_playerActions.Player_Action.Jump.IsPressed())
            Jump();
    }

    private void Jump()
    {
        _rigidbody.velocity = Vector2.up * _jumpHeight;
    }
}
