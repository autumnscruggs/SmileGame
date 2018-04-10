using UnityEngine;
using System.Collections;

public class Potion : CombatItem
{
    public float healingAmount { get; set; }
    
    public Potion()
    {
        this.CanBeUsedInMainInventory = true;
        ItemName = "Potion";
        healingAmount = 5;
    }

    public override void Use()
    {
       // Debug.Log("Used potion");

        if (this.UserExists())
        {
            User.RecoverHealth(healingAmount);
        }

        base.Use();
    }
}
