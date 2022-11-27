using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Acting : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpHeight = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    //private PlayerMovement _playerActions;
    private Rigidbody2D _rigidbody;

    private float horizontal;

    private bool _isFacingRight;
    //private Vector2 _moveInput;

 
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpHeight);
        /*if (context.canceled && _rigidbody.velocity.y > 0)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);*/
    }   
    
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    void Update()
    {
        _rigidbody.velocity = new Vector2(horizontal * speed, _rigidbody.velocity.y);
        if (!_isFacingRight && horizontal > 0f) Flip();
        else if (_isFacingRight && horizontal < 0f) Flip();
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("button"))
            ClickButton();
        else if (other.gameObject.CompareTag("stone"))
            CollectStone(other);
        else if (other.gameObject.CompareTag("act"))
            Act(other);

    }

    private void Act(Collision other)
    {
        Destroy(other.gameObject);
    }

    private void CollectStone(Collision other)
    {
        Destroy(other.gameObject);
    }

    private void ClickButton()
    {
        
    }
}
