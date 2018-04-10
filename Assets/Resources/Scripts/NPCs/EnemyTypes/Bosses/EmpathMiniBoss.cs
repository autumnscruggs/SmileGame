using UnityEngine;
using System.Collections;

public class EmpathMiniBoss : MiniBoss
{
    private float damageTaken;
    private float prevPlayerHealth;

    public EmpathMiniBoss()
    {
        this.commanderName = "Empath";
        this.specialMoveName = "Detected Your Aura!";
        this.healMoveName = "Felt Your Pain!";
        this.damageMoveName = "Empathized!";
    }

    protected override void DecideIfSpecialMove()
    {
        int random = Random.Range(0, 4); //made a smaller chance for aura detection
        if (random == 0)
        {
            specialMoveChosen = true;
        }
        else
        {
            specialMoveChosen = false;
        }
    }

    protected override void FullAIDecisionTree()
    {
        if (healAmountWhenCry <= 0) { this.healMoveName = "Felt Your Pain! ...but nothing happened!"; }
        else { this.healMoveName = "Felt Your Pain!"; }
        base.FullAIDecisionTree();
        ResetSpecialValues();
    }

    public override void SpecialMove()
    {
        this.RecoverHealth(damageTaken / 2);
        this.specialMoveDamage = damageTaken;
    }

    protected override void SpecialMoveSequence()
    {
        if(damageTaken == 0)
        {
            float healAmount = this.target.Health - prevPlayerHealth;
            if(healAmount > 0) { this.RecoverHealth(healAmount / 2); }
            this.ExecuteSpecialMove(true);
        }
        else
        {
            this.ExecuteSpecialMove(true);
        }
    }

    protected override void AttackMoveTree()
    {
        base.AttackMoveTree();
    }


    private void ResetSpecialValues()
    {
        this.damageTaken = 0;
        this.attackDamage = 0;
        this.healAmountWhenCry = 0;
        prevPlayerHealth = this.Target.Health;
    }

    public override void HealMove()
    {
        this.healAmountWhenCry /= 2; //half player attack absorption because holy shit
        base.HealMove();
    }

    public override void TakeDamage(float damageAmount)
    {
        damageTaken = damageAmount;
        this.healAmountWhenCry = damageTaken;
        base.TakeDamage(damageAmount);
    }

    public override void DamageMove()
    {
        float damage = target.Health - this.health;
        if(damage > 0)
        {
            this.attackDamage = damage;
            base.DamageMove();
        }
        else
        {
            this.healAmountWhenCry = Mathf.Abs(damage);
            base.HealMove();
        }
        
    }
}
