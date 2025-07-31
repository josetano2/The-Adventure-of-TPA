using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMovementController : MonoBehaviour
{
    // main controller
    private CharacterController controller;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    private Vector3 playerVelocity;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private RaycastHit groundHit;

    // animator player
    private Animator animator;
    public Animator AnimatorPlayer
    {
        get { return animator; }
        set { animator = value; }
    }

    // condition
    private bool isJumping;
    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }
    private bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
    public bool canAttack;
    private float attackCooldown = 1.0f;

    // switch player script reference
    private PlayerManager playerManager;

    private bool isAttacking = false;
    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }
    private bool isHeavy = false;
    public bool IsHeavy
    {
        get { return isHeavy; }
        set { isHeavy = value; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerManager = FindObjectOfType<PlayerManager>();
        Cursor.visible = false;
    }

    void Update()
    {
        playerMovement();
        checkState();
        attackState();
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
            if (Input.GetButtonDown("Jump") && playerManager.ActiveController != playerManager.AraszkiewiczController)
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

    void attackState()
    {
        isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        //if (!isMoving)
        //{
            if (Input.GetMouseButtonDown(0))
            {
                if (canAttack)
                {
                    attack();
                }
            }

            else if (Input.GetMouseButtonDown(1))
            {
                if (canAttack)
                {
                    heavyAttack();
                }
            }
        //}
    }

    void attack()
    {
        isAttacking = true;
        canAttack = false;
        isHeavy = false;
        animator.SetTrigger("Attack");
        StartCoroutine(resetAttackCooldown());
    }

    void heavyAttack()
    {
        isAttacking = true;
        canAttack = false;
        isHeavy = true;
        animator.SetTrigger("Heavy Attack");
        StartCoroutine(resetAttackCooldown());
    }

    IEnumerator resetAttackCooldown()
    {
        StartCoroutine(resetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        isHeavy = false;
    }

    IEnumerator resetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
