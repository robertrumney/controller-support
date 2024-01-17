using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

public class CloseOnEscape : MonoBehaviour
{
    public AudioSource sawc;
    public GameObject makeActive;

    private void Update()
    {
        // Check if the Escape key is pressed using the new Input System
        if (Keyboard.current.escapeKey.wasPressedThisFrame || IsGamepadBackPressed())
        {
            if (sawc)
                sawc.Play();

            if (makeActive)
                makeActive.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    private bool IsGamepadBackPressed()
    {
        // Check if at least one gamepad is connected
        if (Gamepad.all.Count > 0)
        {
            // Iterate through all connected gamepads
            foreach (var gamepad in Gamepad.all)
            {
                // Check if the back button (typically 'select' or similar) is pressed
                if (gamepad.selectButton.wasPressedThisFrame)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
