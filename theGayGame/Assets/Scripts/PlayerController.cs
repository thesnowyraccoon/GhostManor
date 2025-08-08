using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

//Reference Brackeys

public class PlayerController : MonoBehaviour
{
    // Inputs
    [Header("Inputs")]
    public InputAction movementAction;
    public InputAction sprintAction;
    public InputAction jumpAction;
    public InputAction lookAction;

    // GameObjects/Components
    [Header("Components")]
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;

    // Movement/Look Speeds
    [Header("Movement Speeds")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float lookSpeed = 100f;

    // Movement Settings
    float currentSpeed;
    Vector3 velocity;

    float yRotation = 0f;   // Look rotation

    // Jump Settings
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.8f;

    public LayerMask groundMask;    // Collider check for ground

    // Ground Settings
    float groundDistance = 0.4f;    
    bool isGrounded;

    void Start()
    {
        // Initialize Inputs
        movementAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();

        Cursor.lockState = CursorLockMode.Locked;   // Set cursor in game window

        currentSpeed = moveSpeed;   // Initializes movement to default speed
    }

    void Update()
    {
        Ground();

        Move();
        Look();

        Sprint();
        Jump();
    }

    void Move() // Player movement according to Unity Input System
    {
        Vector3 moveValue = movementAction.ReadValue<Vector3>();    // Reads Unity Input System values
        Vector3 moveDirection = transform.right * moveValue.x + transform.forward * moveValue.z; // Move directions

        controller.Move(moveDirection * currentSpeed * Time.deltaTime); // Player movement

        velocity.y += gravity * Time.deltaTime; // Gravity calculations

        controller.Move(velocity * Time.deltaTime); // Player gravity
    }

    void Sprint()   // Player sprint on input
    {
        if (sprintAction.IsPressed())   // Input check
        {
            currentSpeed = sprintSpeed; // Increase player speed 
        }
        else
        {
            currentSpeed = moveSpeed;   // Sets to default player speed
        }
    }

    void Jump() // Player jump on input
    {
        if (jumpAction.IsPressed() && isGrounded)   // Input and ground check
        {
            //jump code
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);    // Player jumps and is affected by gravity
        }
    }

    void Look() // Player look/camera acccording to Unity Input System
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>() * lookSpeed * Time.deltaTime;   // Reads value from Unity Input System

        yRotation -= lookValue.y;
        yRotation = Mathf.Clamp(yRotation, -70f, 70f);  // Look direction in the y-axis, constrained at angles

        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f); // Camera movement in the y-axis
        player.Rotate(Vector3.up * lookValue.x);    // Player and camera movement in the x-axis
    }

    void Ground()   // Check for ground
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Collision check for ground

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;   
        }
    }
}
