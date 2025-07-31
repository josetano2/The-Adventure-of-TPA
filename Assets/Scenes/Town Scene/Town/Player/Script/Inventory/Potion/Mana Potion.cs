using UnityEngine.UI;
using UnityEngine;

public class ManaPotion : Potion
{
    //public int manaBuff { get; private set; }
    [SerializeField] private float manaBuff;

    public ManaPotion(int id, string name, int price, Sprite img,int manaBuff) : base(id, name, price, img)
    {
        this.manaBuff = manaBuff;
    }

    public override void potionBuff()
    {
        if (playerManager.ActivePlayer.CurrMana < playerManager.ActivePlayer.Mana)
        {
            playerManager.ActivePlayer.CurrMana += manaBuff;
            if (playerManager.ActivePlayer.CurrHP > playerManager.ActivePlayer.Mana)
            {
                playerManager.ActivePlayer.CurrHP = playerManager.ActivePlayer.Mana;
            }
        }

    }

    public override bool validateStat()
    {
        if (playerManager.ActivePlayer.CurrMana == playerManager.ActivePlayer.Mana)
        {
            return false;
        }
        return true;
    }
}