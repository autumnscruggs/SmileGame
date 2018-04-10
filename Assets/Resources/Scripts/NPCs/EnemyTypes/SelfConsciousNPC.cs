using UnityEngine;
using System.Collections;

public class SelfConsciousNPC : NPC
{
    private bool healingInProgress;
    private int healingTurnsPassed;
    public int amountOfTurnsToHeal;
    public float insultSelfHeal = 2;
    private float originalAttackDamage;
    private string originalDamageMoveName;
    private bool justTookDamage;

    private float ifHurtAttackDamage = 2;

    public SelfConsciousNPC()
    {
        this.commanderName = "Self-Conscious Crier";
        this.damageMoveName = "Looked In A Mirror!";
        originalDamageMoveName = this.damageMoveName;
        this.healMoveName = "Hid Their Face!";

        originalAttackDamage = this.attackDamage;
        justTookDamage = false;
        healingInProgress = false;
        healingTurnsPassed = 0;
        amountOfTurnsToHeal = 3;
        ifHurtAttackDamage = this.attackDamage * 2;
    }

    public override void TakeDamage(float damageAmount)
    {
        justTookDamage = true;
        base.TakeDamage(damageAmount);
    }

    public override void HealMove()
    {
        healingInProgress = true;
        base.HealMove();
    }

    private void DecideToHealOrNotToHeal() //Simple random roll
    {
        int random = Random.Range(0, 4); 
        if (random == 0) // 1/4 chance to heal
        {
            AttackMove();
        }
        else if(random == 1) //1/4 chance not to attack, because self consious
        {
            InsultSelf();
        }
        else //1/2 chance to heal
        {
            CommandCreator.God.ExecuteNPCHeal(this.gameObject, healMoveName);
        }
    }

    protected override void FullAIDecisionTree() //Overriding AI for specific NPC
    {
        if (!healingInProgress)
        {
            if (healingTurnsPassed >= amountOfTurnsToHeal)
            {
                this.damageMoveName = originalDamageMoveName + " And Stopped Hiding!";
                healingTurnsPassed = 0;
                AttackMove();
            }
            else
            {
                this.damageMoveName = originalDamageMoveName + "!";
                DecideToHealOrNotToHeal();
            }

        }
        else
        {
            this.damageMoveName = originalDamageMoveName + " And Hid Again!";

            healingTurnsPassed++;
            this.RecoverHealth(healAmountWhenCry / 2);

            if (healingTurnsPassed >= amountOfTurnsToHeal)
            {
                healingInProgress = false;
            }

            AttackMove();
        }

        justTookDamage = false;
    }

    private void AttackMove()
    {
        if (justTookDamage) { this.attackDamage = ifHurtAttackDamage; }
        else { this.attackDamage = originalAttackDamage; }
        CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
    }

    private void InsultSelf()
    {
        Debug.Log("Not attacking");
        this.attackDamage = 0;
        this.damageMoveName = "Insulted Themselves! You feel better somehow...";
        this.target.RecoverHealth(insultSelfHeal);
        this.RecoverHealth(insultSelfHeal);
        CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
    }
}

