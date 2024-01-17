using UnityEngine;
using UnityEngine.UI;

// Import the new Input System namespace
using UnityEngine.InputSystem;

public class GamepadLabel : MonoBehaviour
{
    // Assignable in editor
    [SerializeField] private string gamepadText = "Gamepad Enabled";

    // Assignable in editor
    [SerializeField] private string keyboardText = "Keyboard Enabled";

    // Reference to the Text component
    private Text labelText;

    // Flag to indicate gamepad connection
    private bool isGamepadConnected = false;

    private void OnEnable()
    {
        // Get the Text component attached to the same GameObject
        labelText = GetComponent<Text>();
        if (labelText == null)
        {
            Debug.LogError("Text component not found on the GameObject");
        }

        // Initial check for gamepad connection
        isGamepadConnected = Gamepad.current != null;

        // Update text based on the device connection
        labelText.text = isGamepadConnected ? gamepadText : keyboardText;
    }
}
