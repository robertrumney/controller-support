using UnityEngine;

public class RawInputDetector : MonoBehaviour
{
    void Update()
    {
        // Detect all buttons
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("Button " + i + " pressed");
            }
        }

        // Detect all axes
        for (int i = 1; i <= 10; i++)
        {
            float axisValue = Input.GetAxisRaw("Axis " + i);
            if (Mathf.Abs(axisValue) > 0.2f) // Using a deadzone
            {
                Debug.Log("Axis " + i + " value: " + axisValue);
            }
        }
    }
}
