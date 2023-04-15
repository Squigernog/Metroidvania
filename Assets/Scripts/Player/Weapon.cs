using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;
    private bool weaponEnabled;
    private Quaternion rot;

    [Header("Input")]
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    public void EnableWeapon()
    {
        weaponEnabled = true;
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
        if (!weaponEnabled)
        {
            return;
        }

        Debug.Log("Fire");
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }
}
