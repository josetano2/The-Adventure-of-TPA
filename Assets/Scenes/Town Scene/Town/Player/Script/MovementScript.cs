using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 6f;
    [SerializeField] private Transform cam;

    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;

    // Animation
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        //Debug.Log(animator);
    }

    void Update()
    {
        playerMovement();
        checkState();
    }

    void playerMovement()
    {

        float horizontal = Input.GetAxisRaw("Horizontal"); // a d
        float vertical = Input.GetAxisRaw("Vertical"); // w s

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetButtonDown("Jump");
        bool isGrounded = controller.isGrounded;

            if (shiftPressed)
            {
                speed = 9f;
            }
            if (!shiftPressed)
            {
                speed = 6f;
            }

        if (direction.magnitude >= 0.1f)
        {
            // movement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float targetHeight = hit.point.y;
                Vector3 currentPosition = transform.position;
                currentPosition.y = targetHeight;
                transform.position = currentPosition;
            }

        }

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
    }
}