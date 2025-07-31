using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] CircleInteract circleInteract;
    [SerializeField] GameObject missionUI;
    private bool isMissionOpen = false;
    public bool IsMissionOpen
    {
        get { return isMissionOpen; }
    }

    [SerializeField] GameObject inventoryUI;
    private bool isInventoryOpen = false;
    public bool IsInventoryOpen
    {
        get { return isInventoryOpen; }
    }

    [SerializeField] GameObject pauseUI;
    private bool isPauseOpen = false;
    public bool IsPauseOpen
    {
        get { return isPauseOpen; }
    }

    private MovementController movementController;
    public MovementController MovementController
    {
        get { return movementController; }
    }
    private Animator animator;
    private CinemachineFreeLook freeLookCam;
    public CinemachineFreeLook FreeLookCam
    {
        get { return freeLookCam; }
    }

    private RaycastHit groundHit;

    [SerializeField] private AudioSource bgm;
    public AudioSource Bgm
    {
        get { return bgm; }
    }
    void Start()
    {
        missionUI.SetActive(false);
        inventoryUI.SetActive(false);
        pauseUI.SetActive(false);
        movementController = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
        freeLookCam = FindObjectOfType<CinemachineFreeLook>();
        if(SettingManager.settingInstance != null)
        {
            bgm.volume = SettingManager.settingInstance.volumeValue;
        }
    }

    void Update()
    {
        bool groundCheck = Physics.Raycast(transform.position, -Vector3.up, out groundHit, 0.1f);

        if (Input.GetKeyDown(KeyCode.I) && !isPauseOpen)
        {
            toggleInventoryUI();
            if (isInventoryOpen)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseUI();
            if (isMissionOpen)
            {
                toggleMissionUI();
            }
            if (circleInteract.IsShopOpen)
            {
                circleInteract.toggleShopUI();
            }
            
            if (isPauseOpen)
            {
                Cursor.visible = true;
                movementController.enabled = false;
                freeLookCam.enabled = false;
                bgm.Pause();
            }
            else
            {
                Cursor.visible = false;
                movementController.enabled = true;
                freeLookCam.enabled = true;
                bgm.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && !isPauseOpen)
        {
            if (!animator.GetBool("isJumping") && groundCheck) // Check if the character is not jumping
            {
                float interactRange = 2f;
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray)
                {
                    if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                    {
                        bool isWalking = animator.GetBool("isWalking");
                        bool isRunning = animator.GetBool("isRunning");
                        bool isGrounded = animator.GetBool("isGrounded");
                        bool isFalling = animator.GetBool("isFalling");
                        toggleMissionUI();
                        if (isMissionOpen)
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
                            if (isGrounded)
                            {
                                animator.SetBool("isGrounded", false);
                            }
                            if (isFalling)
                            {
                                animator.GetBool("isFalling");
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
        }
        
    }

    void toggleMissionUI()
    {
        isMissionOpen = !isMissionOpen;
        missionUI.SetActive(isMissionOpen);
    }

    void toggleInventoryUI()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
    }

    public void togglePauseUI()
    {
        isPauseOpen = !isPauseOpen;
        pauseUI.SetActive(isPauseOpen);
    }
}
