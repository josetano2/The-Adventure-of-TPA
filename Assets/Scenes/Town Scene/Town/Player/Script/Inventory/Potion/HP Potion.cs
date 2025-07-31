using UnityEngine.UI;
using UnityEngine;

public class HPPotion : Potion
{
    //public int hpBuff { get; private set; }
    [SerializeField] private float hpBuff;

    public HPPotion(int id, string name, int price, Sprite img,int hpBuff) : base(id, name, price, img)
    {
        this.hpBuff = hpBuff;
    }

    public override void potionBuff()
    {
        if(playerManager.ActivePlayer.CurrHP < playerManager.ActivePlayer.HP)
        {
            playerManager.ActivePlayer.CurrHP += hpBuff;
            if(playerManager.ActivePlayer.CurrHP > playerManager.ActivePlayer.HP)
            {
                playerManager.ActivePlayer.CurrHP = playerManager.ActivePlayer.HP;
            }
        }
        
    }

    public override bool validateStat()
    {
        if(playerManager.ActivePlayer.CurrHP == playerManager.ActivePlayer.HP)
        {
            return false;
        }
        return true;
    }

}