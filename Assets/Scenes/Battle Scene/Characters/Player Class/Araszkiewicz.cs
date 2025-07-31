using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Araszkiewicz : Player
{
    private Canvas canvas;
    private Bar3DManager bar3dManager;
    void Start()
    {
        CurrHP = HP;
        CurrMana = Mana;
        canvas = GetComponentInChildren<Canvas>();
        bar3dManager = FindObjectOfType<Bar3DManager>();
        bar3dManager.setMaxHealth(HP);
        bar3dManager.setHealth(HP);
    }


    void Update()
    {
        if (CurrHP <= 0)
        {
            removePlayer();
        }
        canvas.transform.position = transform.position + Vector3.up * 2f;
        bar3dManager.setHealth(CurrHP);
    }
}
