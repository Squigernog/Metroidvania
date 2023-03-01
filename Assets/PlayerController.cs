using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpSpeed;
    private Vector2 _moveDirection;

    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Input")]
    private PlayerInputActions _playerInputActions;
    private InputAction _movement;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _movement = _playerInputActions.Player.Move;
        _movement.Enable();

        _playerInputActions.Player.Jump.performed += DoJump;
        _playerInputActions.Player.Jump.canceled += DoJump;
        _playerInputActions.Player.Jump.Enable();

        _playerInputActions.Player.Fire.performed += Fire;
        _playerInputActions.Player.Fire.Enable();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void DoJump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if(context.performed && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        if(context.canceled && rb.velocity.y > 0f)
        {
            Debug.Log("Jump Canceled");
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);
        }
            
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    private void OnDisable()
    {
        _movement.Disable();
        _playerInputActions.Player.Jump.Disable();
        _playerInputActions.Player.Fire.Disable();
    }
    private void Update()
    {
        //Debug.Log("Movement Values " + _movement.ReadValue<Vector2>());
        _moveDirection = _movement.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, rb.velocity.y);
    }
}
