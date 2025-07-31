using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private PlayerManager pm;
    [SerializeField] private CrystalManager cm;
    [SerializeField] private PindahScene sm;
    void Start()
    {
        
    }
    void Update()
    {
        if((pm.tim.CurrHP <= 0 && pm.patrick.CurrHP <= 0 && pm.araszkiewicz.CurrHP <= 0) || (cm.CrystalCurrHP <= 0))
        {
            Debug.Log("mati smua");
            sm.gameOverScene();
        }
    }
}
