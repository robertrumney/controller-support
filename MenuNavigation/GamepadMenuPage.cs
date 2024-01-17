using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; 

using System.Collections;

public class GamepadMenuPage : MonoBehaviour
{
    // Assign in Unity Editor
    public GameObject initialSelectedElement;
    public GamepadMenuPage optionalSelectOnDisable;

    public void OnEnable()
    {
        if(enabled)
            StartCoroutine(OnEnableRoutine());
    }

    private void OnDisable()
    {
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
