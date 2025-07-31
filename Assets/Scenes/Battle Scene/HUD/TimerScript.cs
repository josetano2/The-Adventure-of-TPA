using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    public static bool isTimeOn = true;
    public static float currTime = 0;
    public static int enemyKillCount = 0;
    [SerializeField] TMP_Text timeText;

    private static TimerScript timeInstance;
    private void Awake()
    {
        if (timeInstance == null)
        {
            timeInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currTime = 0;
    }
    void Update()
    {
        if (isTimeOn)
        {
            currTime = currTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currTime);
            timeText.text = time.ToString(@"mm\:ss");
        }
    }
}
