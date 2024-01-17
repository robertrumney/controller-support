using System.Collections;

using UnityEngine;
using UnityEngine.UI;

// Import the new Input System namespace
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class OnEnableCutsceneLock : MonoBehaviour
{
    // Array of camera GameObjects to switch between
    public GameObject[] cameras;

    // Index of the currently active camera
    public int currentCam = 0;
    
    // Frequency of camera switches
    public float freq = 5;
   
    // Speed of camera movement
    public float moveSpeed = 1;
   
    // Speed of camera rotation
    public float rotSpeed = 1;

    // Reference to the Text UI for hold-to-skip message
    public Text holdToSkip;
    
    // Reference to an Animation component
    public Animation anim;

    // Reference to the main camera's Transform
    private Transform mainCam;
    
    // Name of the level to load after the cutscene
    private readonly string goToLevel = "Mission 1";

    // Boolean to check if the player can hold to skip
    private bool canHold = true;
    
    // Duration of how long the key/button is held
    private float isHolding;

    private void Awake()
    {
        // Get the main camera's Transform
        mainCam = Camera.main.transform;
    }

    private IEnumerator Start()
    {
        // Hide the cursor
        Cursor.visible = false;

        // Wait for the specified frequency duration
        yield return new WaitForSeconds(freq);

        if (currentCam > cameras.Length - 2)
        {
            // Invoke the Text method after 3 seconds
            Invoke(nameof(Text), 3);
            // Exit the coroutine
            yield break;
        }

        // Move to the next camera
        currentCam++;

        // Restart the coroutine to continue the cycle
        StartCoroutine(Start());
    }

    private void Text()
    {
        // Load the specified level
        LoadingScreen.instance.Load(goToLevel);
    }

    // Reference to the background GameObject for the text
    public GameObject textBG;

    private void Update()
    {
        // Lerp the main camera's position and rotation to the current camera's position and rotation
        mainCam.position = Vector3.Lerp(mainCam.position, cameras[currentCam].transform.position, moveSpeed * Time.deltaTime);
        mainCam.rotation = Quaternion.Slerp(mainCam.rotation, cameras[currentCam].transform.rotation, rotSpeed * Time.deltaTime);

        if (canHold)
        {
            // Check if any key is pressed using the new Input System for Keyboard
            bool isAnyKeyPressed = Keyboard.current.anyKey.isPressed;

            // Check for any button press on the Gamepad, if a Gamepad is connected
            bool isAnyGamepadButtonPressed = false;
            if (Gamepad.current != null)
            {
                foreach (var control in Gamepad.current.allControls)
                {
                    if (control is ButtonControl buttonControl && buttonControl.isPressed)
                    {
                        isAnyGamepadButtonPressed = true;
                        break;
                    }
                }
            }

            // Check if any key is pressed using the new Input System for Keyboard or Gamepad
            if (isAnyKeyPressed || isAnyGamepadButtonPressed)
            {
                // Increment the hold time
                isHolding += Time.deltaTime;

                float subtract = 4 - isHolding;
                // Calculate time left to hold
                float timeLeft = Mathf.Round(subtract);

                // Update the hold-to-skip text
                holdToSkip.text = "HOLD TO SKIP - " + timeLeft.ToString() + " SECONDS LEFT";

                // Show the text background
                textBG.SetActive(true);

                // If held for more than 4 seconds
                if (isHolding > 4)
                {
                    holdToSkip.text = "";
                    canHold = false;

                    // Stop all coroutines
                    StopAllCoroutines();

                    // Load the specified level
                    LoadingScreen.instance.Load(goToLevel);
                }
            }
            else
            {
                // Reset holding variables if no key or button is pressed
                isHolding = 0;
                holdToSkip.text = "";
                textBG.SetActive(false);
            }
        }
    }
}
