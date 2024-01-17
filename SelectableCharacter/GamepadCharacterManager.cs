using UnityEngine;
using System.Collections;

// Import the new Input System namespace
using UnityEngine.InputSystem;

public class GamepadCharacterManager : MonoBehaviour
{
    public SelectableCharacter[] characters;
    private int currentCharacterIndex = 0;

    // Adjust this value for stick sensitivity
    private readonly float stickDeadzone = 0.1f; 

    // Time in seconds between inputs
    private readonly float inputCooldown = 0.3f; 

    private float lastInputTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        UpdateCharacterSelection();
    }

    private void Update()
    {
        if (IsGamepadInputDetected() && Time.time - lastInputTime > inputCooldown)
        {
            float maxInput = GetMaxInput(Gamepad.current.leftStick.y.ReadValue(),
                                         Gamepad.current.rightStick.y.ReadValue(),
                                         Gamepad.current.dpad.y.ReadValue(),
                                         Gamepad.current.leftStick.x.ReadValue(),
                                         Gamepad.current.rightStick.x.ReadValue(),
                                         Gamepad.current.dpad.x.ReadValue());

            if (Mathf.Abs(maxInput) > stickDeadzone)
            {
                int direction = maxInput > 0 ? 1 : -1;
                CycleCharacter(direction);
                lastInputTime = Time.time;
            }
        }

        if (WasGamepadButtonPressed())
        {
            OnSelectCharacter();
        }
    }

    private float GetMaxInput(params float[] inputs)
    {
        float maxInput = 0f;
        foreach (var input in inputs)
        {
            if (Mathf.Abs(input) > Mathf.Abs(maxInput))
            {
                maxInput = input;
            }
        }
        return maxInput;
    }

    private void CycleCharacter(int direction)
    {
        characters[currentCharacterIndex].OnMouseExit();

        currentCharacterIndex += direction;
        if (currentCharacterIndex >= characters.Length)
        {
            currentCharacterIndex = 0;
        }
        else if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = characters.Length - 1;
        }

        UpdateCharacterSelection();
    }

    private void UpdateCharacterSelection()
    {
        characters[currentCharacterIndex].OnMouseEnter();
    }

    public void OnSelectCharacter()
    {
        // Call this method when the select button is pressed
        characters[currentCharacterIndex].OnMouseDown();
    }

    private bool IsGamepadInputDetected()
    {
        if (Gamepad.current != null)
        {
            return Mathf.Abs(GetMaxInput(Gamepad.current.leftStick.x.ReadValue(),
                                         Gamepad.current.rightStick.x.ReadValue(),
                                         Gamepad.current.dpad.x.ReadValue(),
                                         Gamepad.current.leftStick.y.ReadValue(),
                                         Gamepad.current.rightStick.y.ReadValue(),
                                         Gamepad.current.dpad.y.ReadValue())) > stickDeadzone;
        }

        return false;
    }

    private bool WasGamepadButtonPressed()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            // Check specific buttons: A, B, X, Y, and triggers
            if (gamepad.buttonSouth.wasPressedThisFrame || // A
                gamepad.buttonWest.wasPressedThisFrame ||  // X
                gamepad.buttonNorth.wasPressedThisFrame || // Y
                gamepad.buttonEast.wasPressedThisFrame ||  // B
                gamepad.leftTrigger.wasPressedThisFrame ||
                gamepad.rightTrigger.wasPressedThisFrame)
            {
                return true;
            }
        }
        return false;
    }
}
