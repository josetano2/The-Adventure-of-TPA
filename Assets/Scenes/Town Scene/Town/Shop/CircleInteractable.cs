using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CircleInteract : MonoBehaviour
{

    [SerializeField] private bool isPlayerInside = false;
    [SerializeField] GameObject shopUI;
    [SerializeField] PlayerInteract playerInteract;
    private bool isShopOpen = false;
    public bool IsShopOpen
    {
        get { return isShopOpen; }
    }

    private MovementController movementController;
    private Animator animator;
    private CinemachineFreeLook freeLookCam;

    private RaycastHit groundHit;
    void Start()
    {
        shopUI.SetActive(false);
        movementController = FindObjectOfType<MovementController>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        freeLookCam = FindObjectOfType<CinemachineFreeLook>();
    }

    void Update()
    {
        bool groundCheck = Physics.Raycast(transform.position, -Vector3.up, out groundHit, 0.1f);

        if (Input.GetKeyDown(KeyCode.F) && isPlayerInside && !playerInteract.IsPauseOpen)
        {
            if (!animator.GetBool("isJumping") && groundCheck) // Check if the character is not jumping
            {
                Debug.Log("Hello");
                toggleShopUI();
                bool isWalking = animator.GetBool("isWalking");
                bool isRunning = animator.GetBool("isRunning");
                if (isShopOpen)
                {
                    Cursor.visible = true;
                    movementController.enabled = false;
                    freeLookCam.enabled = false;
                    if (isWalking)
                    {
                        animator.SetBool("isWalking", false);
                    }
                    if (isRunning)
                    {
                        animator.SetBool("isRunning", false);
                    }

                }
                else
                {
                    Cursor.visible = false;
                    freeLookCam.enabled = true;
                    movementController.enabled = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
    public void toggleShopUI()
    {
        isShopOpen = !isShopOpen;
        shopUI.SetActive(isShopOpen);
    }


}
