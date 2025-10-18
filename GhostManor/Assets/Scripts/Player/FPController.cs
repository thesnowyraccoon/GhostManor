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
    [SerializeField] private float lookSensitivity = 20f;
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

    [Header("Animations")]
    public Animator animator;
    public GameObject model;

    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

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

        if (context.performed)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
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
        float mouseX = lookInput.x * currentSensitivity / 100;
        float mouseY = lookInput.y * currentSensitivity / 100;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void SetLookSensivity(float sensitivity)
    {
        lookSensitivity = sensitivity;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            animator.SetBool("isJumping", true);

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("isCrouching", true);

            model.transform.localPosition = new Vector3(model.transform.localPosition.x, -0.55f, model.transform.localPosition.z);
            
            controller.height = crouchHeight;
            currentSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            animator.SetBool("isCrouching", false);

            model.transform.localPosition = new Vector3(model.transform.localPosition.x, -0.915f, model.transform.localPosition.z);

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
            currentSpeed = 0f;
            currentSensitivity = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            hotbar.gameObject.SetActive(false);
        }
        else
        {
            if (currentSpeed == 0) currentSpeed = moveSpeed;
        
            currentSensitivity = lookSensitivity;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            hotbar.gameObject.SetActive(true);
        }
    }
}