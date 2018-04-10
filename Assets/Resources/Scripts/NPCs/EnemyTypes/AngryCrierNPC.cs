using UnityEngine;
using System.Collections;

public class AngryCrierNPC : NPC
{
    public float lashOutHealthLoss;
    public float angryCryDamageAmount;

    public AngryCrierNPC()
    {
        this.commanderName = "Angry Crier";
        this.damageMoveName = "Lash Out!";
        this.healMoveName = "Angry Cry!";
    }

    public override void DamageMove()
    {
        base.DamageMove();
        this.TakeDamage(lashOutHealthLoss);
    }

    public override void HealMove()
    {
        this.DealDamage(angryCryDamageAmount);
        base.HealMove();
    }
}
