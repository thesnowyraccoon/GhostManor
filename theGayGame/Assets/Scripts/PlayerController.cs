using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

//Reference Brackeys

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    InputAction sprintAction;
    InputAction jumpAction;
    InputAction lookAction;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;

    float groundDistance = 0.4f;

    public LayerMask groundMask;

    bool isGrounded;

    float xRotation = 0f;

    float currentSpeed;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float lookSpeed = 100f;

    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private float gravity = -9.8f;

    Vector3 velocity;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        Ground();

        Move();
        Look();

        Sprint();
        Jump();
        
    }

    void Move()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * moveValue.x + transform.forward * moveValue.y;

        // movement code
        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (sprintAction.IsPressed())
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    void Jump()
    {
        if (jumpAction.IsPressed() && isGrounded)
        {
            //jump code
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Look()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>() * lookSpeed * Time.deltaTime;

        xRotation -= lookValue.y;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * lookValue.x);
    }

    void Ground()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
