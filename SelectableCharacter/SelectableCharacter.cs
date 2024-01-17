using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

// This class is responsible for the behavior of selectable characters in the game's UI.
// It manages character selection, interactions, audio feedback, and integrates with game systems like menus, saving/loading, and tutorials.
public class SelectableCharacter : MonoBehaviour
{
    // UI text component to display the character's name.
    public Text nameText;
    // Name of the character.
    public string charName;
    // Gender identifier of the character.
    public int gender;

    // Audio source for playing hover sound effects.
    public AudioSource hover;

    // References to various game objects for managing camera, UI canvases, and visual states of the character.
    public GameObject oldCam;
    public GameObject playerCanvas;
    public GameObject oldCanvas;
    public GameObject lightedVersion;
    public GameObject darkVersion;
    public GameObject newCanvas;
    public GameObject closeThis;
    public GameObject resetThis;

    // Reference to the main menu script.
    public MainMenu mainMenu;
    // Reference to the save game manager script for handling save slots.
    public SaveGameManager saveSlots;
    // Array of other characters that can be selected.
    public SelectableCharacter[] otherChars;

    // Flag to ensure certain preferences are only set once.
    [HideInInspector]
    public bool setPrefsOnce = false;

    // Local flag to determine if the character is active or interactable.
    private bool active = false;
    // Local flag to determine if the game is in tutorial mode.
    private bool tutorial = false;

    // Method to set the tutorial mode based on external input.
    public void SetGameOrTutorial(bool x)
    {
        tutorial = x;
    }

    // Method called when the script is enabled. Starts the Reset coroutine.
    private void OnEnable()
    {
        StartCoroutine(Reset());
    }

    // Coroutine to reset the character's active state after a delay.
    private IEnumerator Reset()
    {
        active = false;
        yield return new WaitForSeconds(0.5f);
        active = true;
    }

    // Reference to the main camera for raycasting.
    private Camera mainCamera;
    // Flag to keep track of mouse over state.
    private bool isMouseOver = false;

    // Method called at the start. Initializes mainCamera.
    private void Start()
    {
        // Assuming you're using the main camera for raycasting.
        mainCamera = Camera.main;
    }

    // Method called every frame. Handles raycasting for mouse interaction.
    private void Update()
    {
        RaycastForMouseInteraction();
    }

    // Method that performs raycasting to detect mouse interaction with the character.
    private void RaycastForMouseInteraction()
    {
        // Create a ray from the mouse position into the game world.
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Perform the raycast.
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the raycast hit this character.
            if (hit.transform == transform)
            {
                // If mouse is over the character for the first time, trigger OnMouseEnter.
                if (!isMouseOver)
                {
                    OnMouseEnter();
                    isMouseOver = true;
                }

                // Check if the left mouse button was pressed while over the character.
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    OnMouseDown();
                }
            }
            // If the mouse is no longer over the character, trigger OnMouseExit.
            else if (isMouseOver)
            {
                OnMouseExit();
                isMouseOver = false;
            }
        }
        // If the raycast did not hit anything and the mouse was previously over the character, trigger OnMouseExit.
        else if (isMouseOver)
        {
            OnMouseExit();
            isMouseOver = false;
        }
    }

    // Method called when the character is clicked.
    public void OnMouseDown()
    {
        // If preferences have not been set, set them now.
        if (!setPrefsOnce)
        {
            PlayerPrefs.SetInt("tutorialGender", gender);
            PlayerPrefs.Save();

            // Ensure preferences are only set once for all characters.
            foreach (SelectableCharacter oc in otherChars)
                oc.setPrefsOnce = true;
        }

        // If in tutorial mode, start the real tutorial.
        if (tutorial)
        {
            mainMenu.RealTutorial();
        }
        // If not in tutorial mode, proceed with normal game setup.
        else
        {
            SaveGame.instance.save.gender = gender;
            closeThis.SetActive(false);
            resetThis.SetActive(true);
            newCanvas.SetActive(true);
            saveSlots.SetSaveOrLoadMode(false);
        }
    }

    // Method called when the mouse enters the character's area.
    public void OnMouseEnter()
    {
        // If the character is not active, do not proceed.
        if (!active)
            return;

        // Play the hover sound effect.
        hover.Play();

        // Activate the lighted version of the character and deactivate the dark version.
        lightedVersion.SetActive(true);
        darkVersion.SetActive(false);
        // Update the name text to show the character's name.
        nameText.text = charName;
    }

    // Method called when the mouse exits the character's area.
    public void OnMouseExit()
    {
        // Deactivate the lighted version of the character and activate the dark version.
        lightedVersion.SetActive(false);
        darkVersion.SetActive(true);
    }
}
