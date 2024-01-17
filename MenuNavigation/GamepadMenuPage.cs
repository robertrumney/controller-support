using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

// Import the new Input System namespace
using UnityEngine.InputSystem;

public class GamepadMenuPage : MonoBehaviour
{
    // Assign in Unity Editor
    public GameObject initialSelectedElement;
    public GamepadMenuPage optionalSelectOnDisable;

    public void OnEnable()
    {
        if (enabled)
            StartCoroutine(OnEnableRoutine());
    }

    private void OnDisable()
    {
        if (Gamepad.current == null)
            return;

        if (optionalSelectOnDisable)
        {
            if (optionalSelectOnDisable.gameObject.activeInHierarchy)
            {
                optionalSelectOnDisable.OnEnable();
            }
        }
    }

    private IEnumerator OnEnableRoutine()
    {
        yield return new WaitForSeconds(0.3f);

        // Check if any Gamepad is connected
        if (Gamepad.current == null)
        {
            // No Gamepad connected, exit the coroutine
            yield break;
        }

        if (initialSelectedElement != null)
        {
            EventSystem.current.SetSelectedGameObject(initialSelectedElement);
        }
    }
}
