using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasManager : MonoBehaviour
{
    public Canvas miniMap;

    [Header("Input")]
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        if (_playerInputActions == null)
            _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // Subscribe methods to button inputs
        _playerInputActions.Player.Map.performed += ToggleMap;
        _playerInputActions.Player.Map.Enable();

        _playerInputActions.Player.Settings.performed += ToggleSettings;
        _playerInputActions.Player.Settings.Enable();
    }

    public void ToggleMap(InputAction.CallbackContext context)
    {
        if (miniMap.enabled == true)
            miniMap.enabled = false;
        else
            miniMap.enabled = true;
    }

    public void ToggleSettings(InputAction.CallbackContext context)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
