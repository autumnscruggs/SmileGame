using UnityEngine;
using System.Collections;

public class DogPhoto : CombatItem
{
    public float damageAmount { get; set; }

    public DogPhoto()
    {
        this.CanBeUsedInMainInventory = false;
        ItemName = "Dog Photo";
        damageAmount = 5;
    }

    public override void Use()
    {
        //Debug.Log("Used dog photo");

        if (this.UserExists())
        {
            User.DealDamage(damageAmount);
        }

        base.Use();
    }
}
