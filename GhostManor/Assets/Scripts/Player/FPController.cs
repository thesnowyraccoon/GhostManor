using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    [SerializeField] private Slider sensitivity;
    [SerializeField] private float verticalLookLimit = 90f;

    private float currentSensitivity;

    [Header("Crouch Settings")]
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] public float standHeight = 2f;
    [SerializeField] float crouchSpeed = 2.5f;

    [Header("Pickup Settings")]
    [SerializeField] private float pickupRange = 3f;

    public Transform holdPoint;
    public Item heldObject;
    

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    [Header("Inventory Settings")]
    public HotbarController hotbar;

    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        currentSpeed = moveSpeed;
        currentSensitivity = lookSensitivity;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandlePickup();
        HandlePause();
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
        float mouseX = lookInput.x * currentSensitivity / 10;
        float mouseY = lookInput.y * currentSensitivity / 10;

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
        else if (context.canceled)
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

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {

            if (hit.collider.CompareTag("Item"))
            {
                Item pickUp = hit.collider.GetComponent<Item>();

                if (pickUp != null && holdPoint.childCount < 3)
                {
                    if (heldObject != null)
                    {
                        heldObject.gameObject.SetActive(false);
                        heldObject = null;
                    }

                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;

                    hotbar.AddItem(pickUp.gameObject);
                }
                else if (holdPoint.childCount >= 3)
                {
                    Debug.Log("Inventory full");
                }
            }
        }
        
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject != null)
        {
            hotbar.RemoveItem(heldObject.gameObject);

            heldObject.Drop();
            heldObject = null;

            hotbar.RebuildHotbar();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void HandlePause()
    {
        if (PauseController.isPaused)
        {
            Time.timeScale = 0f;

            //currentSpeed = 0f;
            currentSensitivity = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;

            //currentSpeed = moveSpeed;
            currentSensitivity = lookSensitivity;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}