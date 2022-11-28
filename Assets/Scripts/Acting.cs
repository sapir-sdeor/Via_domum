using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Acting : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpHeight = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private float _horizontal;
    private bool _isFacingRight;

 
    private void Start()
    {
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
        else if (other.gameObject.CompareTag("blowUp"))
            Act(other);

    }

    private void Act(Collision2D other)
    {
        Destroy(other.gameObject);
        gameObject.AddComponent<DestroyBubble>();
        gameObject.GetComponent<DestroyBubble>().SetCollider(_collider);
       
    }

    private void CollectStone(Collision2D other)
    {
        Destroy(other.gameObject);
    }

    private void ClickButton()
    {
        
    }
}
