using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject titleUI;
    public GameObject buttonUI;
    public GameObject settingsUI;
    void Start()
    {
        buttonUI.SetActive(true);
        titleUI.SetActive(true);
        settingsUI.SetActive(false);
    }
}
