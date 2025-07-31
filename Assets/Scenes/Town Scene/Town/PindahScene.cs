using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PindahScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(3);
        }

    }

    public void pindahScene()
    {
        SceneManager.LoadScene(2);
    }

    public void gameOverScene()
    {
        SceneManager.LoadScene(3);
    }


}
