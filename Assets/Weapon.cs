using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;
    private Quaternion rot;

    [Header("Input")]
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        rot = firePoint.rotation *= Quaternion.Euler(0, 0, 90);
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Fire.performed += Fire;
        _playerInputActions.Player.Fire.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Fire.Disable();
    }

    /// <summary>
    /// Performs fire action for player
    /// </summary>
    /// <param name="context"></param>
    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
        Instantiate(projectile, firePoint.position, rot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
