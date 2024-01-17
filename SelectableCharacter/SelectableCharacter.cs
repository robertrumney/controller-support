using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class SelectableCharacter : MonoBehaviour
{
    public Text nameText;
    public string charName;
    public int gender;

    public AudioSource hover;

    public GameObject oldCam;
    public GameObject playerCanvas;
    public GameObject oldCanvas;
    public GameObject lightedVersion;
    public GameObject darkVersion;
    public GameObject newCanvas;
    public GameObject closeThis;
    public GameObject resetThis;

    public MainMenu mainMenu;
    public SaveGameManager saveSlots;
    public SelectableCharacter[] otherChars;

    [HideInInspector]
    public bool setPrefsOnce = false;

    private bool active = false;
    private bool tutorial = false;

    public void SetGameOrTutorial(bool x)
    {
        tutorial = x;
    }

    private void OnEnable()
    {
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        active = false;
        yield return new WaitForSeconds(0.5f);
        active = true;
    }

    private Camera mainCamera;
    private bool isMouseOver = false;

    private void Start()
    {
        // Assuming you're using the main camera for raycasting
        mainCamera = Camera.main; 
    }

    private void Update()
    {
        RaycastForMouseInteraction();
    }

    private void RaycastForMouseInteraction()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                if (!isMouseOver)
                {
                    OnMouseEnter();
                    isMouseOver = true;
                }

                //OnMouseOver();

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    OnMouseDown();
                }
            }
            else if (isMouseOver)
            {
                OnMouseExit();
                isMouseOver = false;
            }
        }
        else if (isMouseOver)
        {
            OnMouseExit();
            isMouseOver = false;
        }
    }

/*    public void OnMouseOver()
    {
        if (!active)
            return;

        lightedVersion.SetActive(true);
        darkVersion.SetActive(false);
        nameText.text = charName;
    }*/

    public void OnMouseDown()
    {
        if (!setPrefsOnce)
        {
            PlayerPrefs.SetInt("tutorialGender", gender);
            PlayerPrefs.Save();

            foreach (SelectableCharacter oc in otherChars)
                oc.setPrefsOnce = true;
        }

        if (tutorial)
        {
            mainMenu.RealTutorial();
        }
        else
        {
            SaveGame.instance.save.gender = gender;
            closeThis.SetActive(false);
            resetThis.SetActive(true);
            newCanvas.SetActive(true);
            saveSlots.SetSaveOrLoadMode(false);
        }
    }

    public void OnMouseEnter()
    {
        if (!active)
            return;

        hover.Play();

        lightedVersion.SetActive(true);
        darkVersion.SetActive(false);
        nameText.text = charName;
    }

    public void OnMouseExit()
    {
        lightedVersion.SetActive(false);
        darkVersion.SetActive(true);
    }
}
}
