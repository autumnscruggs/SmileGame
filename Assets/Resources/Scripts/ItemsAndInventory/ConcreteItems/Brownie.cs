using UnityEngine;
using System.Collections;

public class Brownie : CombatItem
{
    public float healingAmount { get; set; }
    public float damageAmount { get; set; }

    public Brownie()
    {
        this.CanBeUsedInMainInventory = false;
        ItemName = "Brownie";
        healingAmount = 2;
        damageAmount = 3;
    }

    public override void Use()
    {
        //Debug.Log("Used brownie");

        if (this.UserExists())
        {
            //Debug.Log(User);
            User.DealDamage(damageAmount);
            User.RecoverHealth(healingAmount);
        }

        base.Use();
    }
}
