using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] PlayerInteract playerInteract;
    [SerializeField] CircleInteract circleInteract;

    public void resumeGame()
    {
        playerInteract.togglePauseUI();
        Cursor.visible = false;
        playerInteract.MovementController.enabled = true;
        playerInteract.FreeLookCam.enabled = true;
        playerInteract.Bgm.Play();
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
