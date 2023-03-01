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
    private Vector2 _moveDirection;
    private bool _facingRight;

    [Header("Jump")]
    public float jumpSpeed;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public static bool hasDoubleJumpAbility = true; // verify whether the player has unlocked the double jump ability
    private bool _doubleJumped = false; // checks whether a player has double jumped

    [Header("Misc.")]
    public Transform firePoint;

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

        // Subscribe methods to button inputs
        _playerInputActions.Player.Jump.performed += DoJump;
        _playerInputActions.Player.Jump.canceled += DoJump;
        _playerInputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _playerInputActions.Player.Jump.Disable();
    }

    /// <summary>
    /// Check if player is touching ground on bottom of model
    /// </summary>
    /// <returns>bool</returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    /// <summary>
    /// Performs jump action for player
    /// </summary>
    /// <param name="context">Controller Input</param>
    public void DoJump(InputAction.CallbackContext context)
    {
        // Check if button pressed
        if (context.performed && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        else if (context.performed && !_doubleJumped && hasDoubleJumpAbility)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            _doubleJumped = true;
        }

        // Check if button released & traveling up
        if (context.canceled && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);

        // Reset player double jump
        if (IsGrounded())
            _doubleJumped = false;
    }

    /// <summary>
    /// Flip player model when changing directions
    /// </summary>
    public void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        // Debug.Log("Movement Values " + _movement.ReadValue<Vector2>());
        _moveDirection = _movement.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, rb.velocity.y);

        if (!_facingRight && _moveDirection.x < 0f)
            Flip();
        else if (_facingRight && _moveDirection.x > 0f)
            Flip();
    }
}
