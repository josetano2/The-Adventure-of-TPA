using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private BattleMovementController timController;
    public BattleMovementController TimController
    {
        get { return timController; }
        set { timController = value; }
    }
    [SerializeField] private BattleMovementController patrickController;
    public BattleMovementController PatrickController
    {
        get { return patrickController; }
        set { patrickController = value; }
    }
    [SerializeField] private BattleMovementController araszkiewiczController;
    public BattleMovementController AraszkiewiczController {
        get { return araszkiewiczController; }
        set { araszkiewiczController = value; }
    }


    // camera
    [SerializeField] private CinemachineFreeLook timCam;
    [SerializeField] private CinemachineFreeLook patrickCam;
    [SerializeField] private CinemachineFreeLook araszkiewiczCam;

    // main camera
    private Camera cam;

    private CinemachineFreeLook activeCam;
    public CinemachineFreeLook ActiveCam
    {
        get { return activeCam; }
        set { activeCam = value; }
    }
    private BattleMovementController activeController;
    public BattleMovementController ActiveController
    {
        get { return activeController; }
        set { activeController = value; }
    }

    public Tim tim;
    public Patrick patrick;
    public Araszkiewicz araszkiewicz;
    private Player activePlayer;
    public Player ActivePlayer
    {
        get { return activePlayer; }
        set { activePlayer = value; }
    }

    [SerializeField] private BarManager barManager;
    [SerializeField] private Bar3DManager bar3dManagerPrefab;

    // 3d health bar
    [SerializeField] private Bar3DManager bar3dManagerTim;
    [SerializeField] private Bar3DManager bar3dManagerPatrick;
    [SerializeField] private Bar3DManager bar3dManagerAraszkiewicz;

    public List<Player> listOfPlayer = new List<Player>();


    void Start()
    {
        // dapetin kamera
        cam = GetComponent<Camera>();

        // reference controller per character
        timController = GameObject.Find("Knight").GetComponent<BattleMovementController>();
        patrickController = GameObject.Find("Dog Player").GetComponent<BattleMovementController>();
        araszkiewiczController = GameObject.Find("Wizard").GetComponent<BattleMovementController>();

        // keep track active character
        activeCam = timCam;
        activeController = timController;

        // dapetin game object
        tim = GameObject.Find("Knight").GetComponent<Tim>();
        patrick = GameObject.Find("Dog Player").GetComponent<Patrick>();
        araszkiewicz = GameObject.Find("Wizard").GetComponent<Araszkiewicz>();
        activePlayer = tim;

        // buat 2d bar
        barManager.setMaxHealth(tim.HP);
        barManager.setMaxMana(tim.Mana);

        // buat 3d bar
        bar3dManagerTim.gameObject.SetActive(false);
        bar3dManagerPatrick.gameObject.SetActive(true);
        bar3dManagerAraszkiewicz.gameObject.SetActive(true);

        listOfPlayer.Add(tim);
        listOfPlayer.Add(patrick);
        listOfPlayer.Add(araszkiewicz);
    }

    void Update()
    {
        switchPlayerManager();
        updateBar();
    }

    void switchPlayerManager()
    {
        Player nextPlayer;
        if ((Input.GetKeyDown(KeyCode.Q) && activeController.IsGrounded && !activeController.AnimatorPlayer.GetBool("isFalling") && !activeController.AnimatorPlayer.GetBool("isJumping")))
        {

            nextPlayer = getNextPlayer();  
            switchCharacter(activeController, getPlayerController(nextPlayer), activeCam, getPlayercamera(nextPlayer), nextPlayer);
            switchBar();
        }
        else if(activePlayer.CurrHP <= 0)
        {
            nextPlayer = getNextAlivePlayer();
            switchCharacter(activeController, getPlayerController(nextPlayer), activeCam, getPlayercamera(nextPlayer), nextPlayer);
            switchBar();
        }
        
    }

    Player getNextPlayer()
    {
        int currIdx = listOfPlayer.IndexOf(activePlayer);
        int nextIdx = (currIdx + 1) % listOfPlayer.Count;

        return listOfPlayer[nextIdx];
    }
    Player getNextAlivePlayer()
    {
        int currIdx = listOfPlayer.IndexOf(activePlayer);
        int nextIdx = (currIdx + 1) % listOfPlayer.Count;
        int counter = 0; 
        while(counter < listOfPlayer.Count && (activePlayer == listOfPlayer[nextIdx] || listOfPlayer[nextIdx].CurrHP <= 0))
        {
            nextIdx = (currIdx + 1) % listOfPlayer.Count;
            counter++;
        }
        return listOfPlayer[nextIdx];
    }

    BattleMovementController getPlayerController(Player player)
    {
        if (player == tim)
        {
            return timController;
        }

        else if (player == patrick)
        {
            return patrickController;

        }
        else if (player == araszkiewicz)
        {
            return araszkiewiczController;
        }
        return null;
    }

    CinemachineFreeLook getPlayercamera(Player player)
    {
        if (player == tim)
        {
            return timCam;
        }

        else if (player == patrick)
        {
            return patrickCam;

        }
        else if (player == araszkiewicz)
        {
            return araszkiewiczCam;
        }
        return null;
    }

    void switchCharacter(BattleMovementController currCon, BattleMovementController newCon, CinemachineFreeLook currCam, CinemachineFreeLook newCam, Player newPlayer)
    {
        currCam.Priority = 0;
        newCam.Priority = 10;

        currCon.AnimatorPlayer.SetBool("isWalking", false);
        currCon.AnimatorPlayer.SetBool("isRunning", false);
        currCon.AnimatorPlayer.SetBool("isJumping", false);
        currCon.AnimatorPlayer.SetBool("isGrounded", false);
        currCon.AnimatorPlayer.SetBool("isFalling", false);

        currCon.enabled = false;
        newCon.enabled = true;

        activeController = newCon;
        activeCam = newCam;
        activePlayer = newPlayer;
    }
    void updateBar()
    {
        barManager.setMaxHealth(activeController == timController ? tim.HP : (activeController == patrickController) ? patrick.HP : araszkiewicz.HP);
        barManager.setHealth(activeController == timController ? tim.CurrHP : (activeController == patrickController) ? patrick.CurrHP : araszkiewicz.CurrHP);
        barManager.setMaxMana(activeController == timController ? tim.Mana : (activeController == patrickController) ? patrick.Mana : araszkiewicz.Mana);
        barManager.setMana(activeController == timController ? tim.CurrMana : (activeController == patrickController) ? patrick.CurrMana : araszkiewicz.CurrMana);

        if (activeController == timController)
        {
            bar3dManagerPatrick.setMaxHealth(patrick.HP);
            bar3dManagerPatrick.setHealth(patrick.CurrHP);
            bar3dManagerAraszkiewicz.setMaxHealth(araszkiewicz.HP);
            bar3dManagerAraszkiewicz.setHealth(araszkiewicz.CurrHP);

        }
        else if (activeController == patrickController)
        {
            bar3dManagerTim.setMaxHealth(tim.HP);
            bar3dManagerTim.setHealth(tim.CurrHP);
            bar3dManagerAraszkiewicz.setMaxHealth(araszkiewicz.HP);
            bar3dManagerAraszkiewicz.setHealth(araszkiewicz.CurrHP);
        }
        else if (activeController == araszkiewiczController)
        {
            bar3dManagerTim.setMaxHealth(tim.HP);
            bar3dManagerTim.setHealth(tim.CurrHP);
            bar3dManagerPatrick.setMaxHealth(patrick.HP);
            bar3dManagerPatrick.setHealth(patrick.CurrHP);
        }
    }

    void switchBar()
    {
        if (activeController == timController)
        {
            bar3dManagerTim.gameObject.SetActive(false);
            bar3dManagerPatrick.gameObject.SetActive(true);
            bar3dManagerAraszkiewicz.gameObject.SetActive(true);
        }
        else if(activeController == patrickController)
        {
            bar3dManagerTim.gameObject.SetActive(true);
            bar3dManagerPatrick.gameObject.SetActive(false);
            bar3dManagerAraszkiewicz.gameObject.SetActive(true);

        }
        else if (activeController == araszkiewiczController)
        {
            bar3dManagerTim.gameObject.SetActive(true);
            bar3dManagerPatrick.gameObject.SetActive(true);
            bar3dManagerAraszkiewicz.gameObject.SetActive(false);
        }
    }

    public int getPlayerIndex(Player player)
    {
        return listOfPlayer.IndexOf(player);
    }
}
