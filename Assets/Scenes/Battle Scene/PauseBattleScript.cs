using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBattleScript : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] InventoryScript inventoryScript;
    public void resumeGame()
    {
        inventoryScript.togglePauseUI();
        Cursor.visible = false;
        playerManager.ActiveController.enabled = true;
        Time.timeScale = 1;
        inventoryScript.BattleOST.volume *= 2;
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
