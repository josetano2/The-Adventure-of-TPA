using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    public Slider HpSlider
    {
        get { return hpSlider; }
        set { hpSlider = value; }
    }
    [SerializeField] private Slider manaSlider;
    public Slider ManaSlider
    {
        get { return manaSlider; }
        set { manaSlider = value; }
    }
    [SerializeField] private Slider crystalSlider;
    public Slider CrystalSlider
    {
        get { return crystalSlider; }
        set { crystalSlider = value; }
    }
    private PlayerManager playerManager;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public void setHealth(float health)
    {
        hpSlider.value = health;

        fill.color = gradient.Evaluate(hpSlider.normalizedValue);   
    }

    public void setMaxHealth(float health)
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void setMana(float mana)
    {
        manaSlider.value = mana;
    }

    public void setMaxMana(float mana)
    {
        manaSlider.maxValue = mana;
        manaSlider.value = mana;
    }

    public void setCrystalHP(float crystalHP)
    {
        crystalSlider.value = crystalHP;
    }

    public void setMaxCrystalHP(float crystalHP)
    {
        crystalSlider.maxValue = crystalHP;
        crystalSlider.value = crystalHP;
    }

}
