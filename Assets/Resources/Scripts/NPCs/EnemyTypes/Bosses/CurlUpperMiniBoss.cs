using UnityEngine;
using System.Collections;

public class CurlUpperMiniBoss : MiniBoss
{
    private string specialPrep;
    private int turnsPassedAfterSpecial;
    private int specialMoveTurnDelay = 2;
    private float damageTaken;

    public CurlUpperMiniBoss()
    {
        this.commanderName = "Curl Upper";

        this.damageMoveName = "Broke Down!";
        this.healMoveName = "Curls Up!";

        specialPrep = "Bottled Up Their Feelings!";
        this.specialMoveName = "Released Their Emotions!";

        turnsPassedAfterSpecial = 0;
    }

    public override void TakeDamage(float damageAmount)
    {
        damageTaken = damageAmount;
        base.TakeDamage(damageAmount);
    }

    public override void HealMove()
    {
        this.RecoverHealth(damageTaken / 3); //negate some damage
        base.HealMove(); //as well as heal 
    }

    protected override void SpecialMoveSequence()
    {
        turnsPassedAfterSpecial++;
        if (turnsPassedAfterSpecial >= specialMoveTurnDelay)
        {
            turnsPassedAfterSpecial = 0;
            this.ExecuteSpecialMove(true);
        }
        else
        {
            CommandCreator.God.ExecuteSkipTurn(this.gameObject, specialPrep);
        }
    }

    protected override void AttackMoveTree()
    {
        base.AttackMoveTree();
    }
    
}
