using UnityEngine;

// Needed for Event System
using UnityEngine.EventSystems;

// Import the new Input System namespace
using UnityEngine.InputSystem;  

public class GamepadMenu : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject lastSelectedObject;

    private void Update()
    {
        // Getting the current Event System
        eventSystem = EventSystem.current;

        // Keep track of the currently selected object
        if (eventSystem.currentSelectedGameObject != null)
        {
            lastSelectedObject = eventSystem.currentSelectedGameObject;
        }

        // Check if no UI element is selected and gamepad input is detected
        if (eventSystem.currentSelectedGameObject == null && IsGamepadInputDetected())
        {
            // Reselect the last selected element
            if (lastSelectedObject != null)
            {
                eventSystem.SetSelectedGameObject(lastSelectedObject);
            }
        }
    }

    private bool IsGamepadInputDetected()
    {
        if (Gamepad.current != null)
        {
            // Check for horizontal or vertical input from the left stick
            bool isLeftStickHorizontalInput = Mathf.Abs(Gamepad.current.leftStick.x.ReadValue()) > 0.1f;
            bool isLeftStickVerticalInput = Mathf.Abs(Gamepad.current.leftStick.y.ReadValue()) > 0.1f;

            // Check for horizontal or vertical input from the right stick
            bool isRightStickHorizontalInput = Mathf.Abs(Gamepad.current.rightStick.x.ReadValue()) > 0.1f;
            bool isRightStickVerticalInput = Mathf.Abs(Gamepad.current.rightStick.y.ReadValue()) > 0.1f;

            // Check for input from the D-Pad (arrow buttons)
            bool isDPadHorizontalInput = Mathf.Abs(Gamepad.current.dpad.x.ReadValue()) > 0.1f;
            bool isDPadVerticalInput = Mathf.Abs(Gamepad.current.dpad.y.ReadValue()) > 0.1f;

            return isLeftStickHorizontalInput || isLeftStickVerticalInput ||
                   isRightStickHorizontalInput || isRightStickVerticalInput ||
                   isDPadHorizontalInput || isDPadVerticalInput;
        }

        return false;
    }
}
