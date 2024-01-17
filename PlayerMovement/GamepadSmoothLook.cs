using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadSmoothLook : MonoBehaviour
{
    #region Variables
    // Sensitivity and smoothing
    public float sensitivity = 4.0f;
    public float smoothSpeed = 0.35f;
    private float sensitivityAmt;

    // Input ranges and rotations
    private float minimumX = -360f;
    private float maximumX = 360f;
    private float minimumY = -78f;
    private float maximumY = 85f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    // Recoil variables
    public float recoilX;
    public float recoilY;

    // Inversion
    public bool inverted = false;

    // Original and backup rotations
    private Quaternion originalRotation;
    private Quaternion backup;

    // Input System variables
    private PlayerInput playerInput;
    private InputAction lookAction;

    // Aim assist variables
    public bool enableAimAssist = true;
    public LayerMask aimAssistLayerMask;
    public float aimAssistRadius = 0.5f;
    #endregion

    #region MonoBehaviour BuiltIn
    private void Awake()
    {
        // Initialize components and input actions
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions["Look"];

        // Initialize transformations
        originalRotation = transform.localRotation;
        backup = originalRotation;

        // Initialize sensitivity
        sensitivityAmt = sensitivity;

        // Lock cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            HandleInput();
            ApplyAimAssist();
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }
    }
    #endregion

    #region Input Handling
    private void HandleInput()
    {
        // Read gamepad stick inputs
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        rotationX += lookInput.x * sensitivityAmt * Time.timeScale;
        rotationY += inverted ? -lookInput.y * sensitivityAmt * Time.timeScale : lookInput.y * sensitivityAmt * Time.timeScale;

        // Recoil and clamping
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY - recoilY, maximumY - recoilY);

        // Calculate target rotation
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX + recoilX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY + recoilY, -Vector3.right);

        // Smooth the rotation and apply
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation * xQuaternion * yQuaternion, smoothSpeed * Time.smoothDeltaTime * 60 / Time.timeScale);

        // Lock roll rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
    }
    #endregion

    #region Aim Assist
    private void ApplyAimAssist()
    {
        if (enableAimAssist)
        {
            Vector3 aimAssistDirection = CalculateAimAssistDirection();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimAssistDirection), smoothSpeed * Time.smoothDeltaTime * 60 / Time.timeScale);
        }
    }

    private Vector3 CalculateAimAssistDirection()
    {
        if (Physics.SphereCast(transform.position, aimAssistRadius, transform.forward, out RaycastHit hit, Mathf.Infinity, aimAssistLayerMask))
        {
            Vector3 directionToTarget = hit.collider.transform.position - transform.position;
            return Vector3.Slerp(transform.forward, directionToTarget.normalized, 0.1f); // 0.1f is the assist strength, customizable
        }
        else
        {
            return transform.forward;
        }
    }
    #endregion

    #region Cursor Locking
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    #endregion

    #region Utility
    public static float ClampAngle(float angle, float min, float max)
    {
        angle %= 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
    #endregion
}
