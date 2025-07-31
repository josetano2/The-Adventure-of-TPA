using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    private int scoreValue;
    void Start()
    {
        scoreValue = ((int)TimerScript.currTime * 100) + (TimerScript.enemyKillCount * 500);
        InventoryManager.inventoryInstance.addCoins(scoreValue);
        score.text = scoreValue.ToString();
    }
    void Update()
    {
        
    }
}
