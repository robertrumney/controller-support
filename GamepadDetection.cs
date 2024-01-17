using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadDetection : MonoBehaviour
{
    // Public variable to check if a gamepad is connected
    public bool IsGamepadConnected; 

    // Event for gamepad connection changes
    public event Action<bool> OnGamepadConnectionChanged;

    private void OnEnable()
    {
        // Subscribe to device change events
        InputSystem.onDeviceChange += OnDeviceChange;
        CheckGamepadConnection();
    }

    private void OnDisable()
    {
        // Unsubscribe from device change events
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Check for gamepad connection or disconnection
        if (device is Gamepad)
        {
            if (change == InputDeviceChange.Added)
            {
                // Gamepad connected
                UpdateGamepadConnectionStatus(true);
            }
            else if (change == InputDeviceChange.Removed)
            {
                // Gamepad disconnected
                UpdateGamepadConnectionStatus(false);
            }
        }
    }

    private void UpdateGamepadConnectionStatus(bool isConnected)
    {
        IsGamepadConnected = isConnected;
        OnGamepadConnectionChanged?.Invoke(isConnected);
    }

    private void CheckGamepadConnection()
    {
        // Initial check for any connected gamepads
        IsGamepadConnected = Gamepad.current != null;
    }
}
