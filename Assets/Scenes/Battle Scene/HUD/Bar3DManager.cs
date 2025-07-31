using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar3DManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    public Slider HpSlider
    {
        get { return hpSlider; }
        set { hpSlider = value; }
    }
    private Canvas canvas;
    [SerializeField] private Transform cameraTransform;
    private Transform characterTransform;
    public Gradient gradient;
    public Image fill;


    //private PlayerManager playerManager;    

    void Start()
    {
        cameraTransform = Camera.main.transform;
        canvas = GetComponent<Canvas>();
    }
    void Update()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
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
}
