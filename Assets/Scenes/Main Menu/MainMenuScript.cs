using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager;
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void backButton()
    {
        canvasManager.buttonUI.SetActive(true);
        canvasManager.titleUI.SetActive(true);
        canvasManager.settingsUI.SetActive(false);
    }

    public void optionButton()
    {
        canvasManager.buttonUI.SetActive(false);
        canvasManager.titleUI.SetActive(false);
        canvasManager.settingsUI.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
