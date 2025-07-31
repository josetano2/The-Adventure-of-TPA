using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    [SerializeField] private float crystalHP;
    public float CrystalHP
    {
        get { return crystalHP; }
        set { crystalHP = value; }
    }
    [SerializeField] private float crystalCurrHP;
    public float CrystalCurrHP
    {
        get { return crystalCurrHP; }
        set { crystalCurrHP = value; }
    }

    [SerializeField] private BarManager barManager;

    void Start()
    {
        crystalCurrHP = crystalHP;
        barManager.setMaxCrystalHP(crystalHP);
    }

    // Update is called once per frame
    void Update()
    {
        updateHPBar();
    }

    void updateHPBar()
    {
        barManager.setCrystalHP(crystalCurrHP);
    }

    public void takeDamage(float damage)
    {
        crystalCurrHP -= damage;
    }
}
