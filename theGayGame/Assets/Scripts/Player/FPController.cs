using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    private float currentSpeed;

    [Header("Look Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float verticalLookLimit = 90f;

    [Header("Crouch Settings")]
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] float standHeight = 2f;
    [SerializeField] float crouchSpeed = 2.5f;

    [Header("Pickup Settings")]
    [SerializeField] private float pickupRange = 3f;
    [SerializeField] private Transform holdPoint;

    private PickUpObject heldObject;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandlePickup();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity / 10;
        float mouseY = lookInput.y * lookSensitivity / 10;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            currentSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            currentSpeed = moveSpeed;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    public void HandlePickup()
    {
        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();

                if (pickUp != null)
                {
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;
                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }
}

    // Title: 
    // Author: 
    // Date: August 10 2023
    // Code version: Unknown
    // Availability: 