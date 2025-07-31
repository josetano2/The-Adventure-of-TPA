using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverButtonManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverOST;
    void Start()
    {
        gameOverOST.volume = SettingManager.settingInstance.volumeValue;
    }
    public void retryButton()
    {
        SceneManager.LoadScene(2);
    }

    public void returnButton()
    {
        SceneManager.LoadScene(1);
    }
}
