using UnityEngine;
using UnityEngine.EventSystems; // Needed for Event System
using UnityEngine.UI; // Needed for UI elements

public class MenuPageController : MonoBehaviour
{
    public GameObject initialSelectedElement; // Assign this in the Unity Editor

    private void OnEnable()
    {
        if (initialSelectedElement != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // Clear current selection
            EventSystem.current.SetSelectedGameObject(initialSelectedElement); // Set new selection
        }
    }
}
