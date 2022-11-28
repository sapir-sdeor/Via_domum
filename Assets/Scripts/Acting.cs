using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private PlayerMovement _inputAction;
 
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
        /*if (other.gameObject.CompareTag("button"))
            ClickButton();
        else if (other.gameObject.CompareTag("stone"))
            CollectStone(other);*/
        if (other.gameObject.name == "act")
            Act(other);
    }

    private void Act(Collision2D other)
    {
        //TODO: need to check if the act is active - press on the right key 
        
        Destroy(other.gameObject);
        MechanicFactory mechanicFactory = gameObject.AddComponent<MechanicFactory>();
        ICoreMechanic coreMechanic = mechanicFactory.CreateMechanic(other.gameObject.tag, _collider);
        coreMechanic.ApplyMechanic();
        
    }

    private void CollectStone(Collision2D other)
    {
        Destroy(other.gameObject);
    }

    private void ClickButton()
    {
        
    }
}
