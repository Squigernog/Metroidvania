using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 _moveDirection;
    private bool _facingRight;
    private PlayerState _playerState;

    [Header("Jump")]
    public float jumpSpeed;
    public float wallSlidingSpeed;
    public float wallJumpXSpeedMultiplier;
    public float wallJumpTime;
    public static bool hasDoubleJumpAbility = true; // verify whether the player has unlocked the double jump ability
    private bool _doubleJumped = false; // checks whether a player has double jumped
    private bool _isWallSliding = false;
    private bool _isWallJumping;
    private float _wallJumpDirection;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private bool canDash = true;
    private bool isDashing;
    

    [Header("Input")]
    private PlayerInputActions _playerInputActions;
    private InputAction _movement;

    public enum PlayerState
    {
        Grounded,
        Jumping,
        Dashing,
        WallSliding
    }

    private void Awake()
    {
        if (_playerInputActions == null)
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

        _playerInputActions.Player.Dash.performed += DoDash;
        _playerInputActions.Player.Dash.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _playerInputActions.Player.Jump.Disable();
        _playerInputActions.Player.Dash.Disable();
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
        // Prevent jumping in dash
        if (!isDashing)
        {
            // Check if button pressed
            if (context.performed && IsGrounded() && !_isWallJumping)
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            // Wall jump logic
            else if (context.performed && IsWalled() && !_isWallJumping)
            {
                Debug.Log("Wall Jump");
                StartCoroutine(WallJump());
            }
            else if (context.performed && !_doubleJumped && hasDoubleJumpAbility && !_isWallJumping)
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
    }

    /// <summary>
    /// Checks whether a player has a wall in front if their character
    /// </summary>
    /// <returns>Bool</returns>
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    /// <summary>
    /// Wall sliding logic
    /// </summary>
    private void WallSlide()
    {
        if(!IsGrounded() && _moveDirection.x != 0f)
        {
            _isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    /// <summary>
    /// Wall jump logic
    /// </summary>
    /// <returns></returns>
    private IEnumerator WallJump()
    {
        // Make sure player jumps away from wall
        if (_facingRight)
            _wallJumpDirection = 1;
        else
            _wallJumpDirection = -1;
        

        _isWallJumping = true;
        rb.velocity = new Vector2(_wallJumpDirection * moveSpeed * wallJumpXSpeedMultiplier, jumpSpeed);
        Flip();

        yield return new WaitForSeconds(wallJumpTime);

        _isWallJumping = false;
    }

    public void DoDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
        StartCoroutine(Dash());
    }

    public IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            isDashing = true;
            _playerState = PlayerState.Dashing;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f; // set gravity to 0 during dash

            // Checking direction player is facing allows for dash from standstill
            if (!_facingRight)
                rb.velocity = new Vector2(dashSpeed, 0f); // player will dash at speed equal to dash speed
            else
                rb.velocity = new Vector2(-dashSpeed, 0f); // player will dash at speed equal to dash speed

            yield return new WaitForSeconds(dashTime);
            rb.gravityScale = originalGravity;
            isDashing = false;

            // Dash cooldown
            yield return new WaitForSeconds(dashCooldown);

            isDashing = false;
            // _playerState = notdashing;
            canDash = true;
        }
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

    // TODO: probably make switch statement instead of imbedded if statements based on player state
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!_isWallJumping)
        {
            if (!isDashing)
                rb.velocity = new Vector2(_moveDirection.x * moveSpeed, rb.velocity.y);

            if (!_facingRight && _moveDirection.x < 0f)
                Flip();
            else if (_facingRight && _moveDirection.x > 0f)
                Flip();
        }

        if (IsWalled())
            WallSlide();

        switch (_playerState)
        {
            case PlayerState.Grounded:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Dashing:
                break;
        }

    }
}
