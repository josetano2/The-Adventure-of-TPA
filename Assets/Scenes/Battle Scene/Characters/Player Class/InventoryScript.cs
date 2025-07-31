using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    [SerializeField] GameObject inventoryUI;
    [SerializeField] PlayerManager playerManager;
    private bool isInventoryOpen = false;

    [SerializeField] GameObject pauseUI;
    private bool isPauseOpen = false;

    private MovementController movementController;
    private Animator animator;
    private CinemachineFreeLook freeLookCam;

    private RaycastHit groundHit;

    [SerializeField] private AudioSource battleOST;
    public AudioSource BattleOST
    {
        get { return battleOST; }
    }

    void Start()
    {
        inventoryUI.SetActive(false);
        pauseUI.SetActive(false);
        movementController = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
        freeLookCam = FindObjectOfType<CinemachineFreeLook>();
        if (SettingManager.settingInstance != null)
        {
            battleOST.volume = SettingManager.settingInstance.volumeValue;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
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
            if (isInventoryOpen)
            {
                toggleInventoryUI();
            }
            if (isPauseOpen)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                playerManager.ActiveController.enabled = false;
                battleOST.volume /= 2;
            }
            else
            {
                Cursor.visible = false;
                playerManager.ActiveController.enabled = true;
                Time.timeScale = 1;
                battleOST.volume *= 2;
            }
        }
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
