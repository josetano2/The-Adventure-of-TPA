using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    private Vector3 playerVelocity;
    public Vector3 PlayerVelocity
    {
        get { return playerVelocity; }
        set { playerVelocity = value; }
    }

    //private bool groundedPlayer;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private RaycastHit groundHit;

    private Animator animator;
    public Animator AnimatorPlayer
    {
        get { return animator; }
        set { animator = value; }
    }

    bool isJumping;
    public bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void Update()
    {
        playerMovement();
        checkState();
    }

    void playerMovement()
    {
        //groundedPlayer = controller.isGrounded;
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, out groundHit, 0.1f);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

        if (shiftPressed)
        {
            speed = 5f;
        }
        if (!shiftPressed)
        {
            speed = 3f;
        }

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        Vector3 movementDirection = movementInput.normalized;

        if (horizontal == 0 && vertical == 0)
        {
            movementDirection = Vector3.zero;
        }

        controller.Move(movementDirection * speed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
            }
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    void checkState()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");

        bool forwardPressed = Input.GetKey("w") ? Input.GetKey("w") : Input.GetKey("s");
        bool sidewayPressed = Input.GetKey("a") ? Input.GetKey("a") : Input.GetKey("d");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

        if (!isWalking && (forwardPressed || sidewayPressed))
        {
            animator.SetBool("isWalking", true);
        }
        if (isWalking && (!forwardPressed && !sidewayPressed))
        {
            animator.SetBool("isWalking", false);
        }

        if (!isRunning && ((forwardPressed || sidewayPressed) && shiftPressed))
        {
            animator.SetBool("isRunning", true);
        }

        if (isRunning && ((!forwardPressed && !sidewayPressed) || !shiftPressed))
        {
            animator.SetBool("isRunning", false);
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isFalling", !isGrounded && playerVelocity.y < 0);
    }
}
