using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem; // Import the new Input System namespace

public class TestingTool : MonoBehaviour
{
    public UnityEvent unityEvent;
    public Key testKey; // Use Key instead of KeyCode for the new Input System

    private void Update()
    {
        // Check if the specified key is pressed
        if (Keyboard.current[testKey].wasPressedThisFrame)
        {
            unityEvent.Invoke();
        }

        // Check if at least one gamepad is connected
        if (Gamepad.all.Count > 0)
        {
            // Iterate through all connected gamepads
            foreach (var gamepad in Gamepad.all)
            {
                // Check if the back button (typically 'select' or similar) is pressed
                if (gamepad.selectButton.wasPressedThisFrame)
                {
                    unityEvent.Invoke();
                }
            }
        }
    }
}
