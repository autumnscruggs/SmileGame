using UnityEngine;
using System.Collections;

public class HyperventilatorMiniBoss : MiniBoss
{
    private float originalSpecialDamage;
    private string originalSpecialName;
    public float panicDamageIncreaseRate;
    public float healAmountAfterCeasingToPanic = 1f;

    private string specialMoveIncrementName;
    private string specialMoveFinish;
    private int turnsPassedAfterSpecial;
    private int specialMoveTurnDelay = 3;

    public HyperventilatorMiniBoss()
    {
        this.commanderName = "Hyperventilator";
        this.specialMoveName = "Panicked!";
        originalSpecialName = this.specialMoveName;
        specialMoveIncrementName = "Panicked Harder!";
        specialMoveFinish = "Stopped Panicking!";
        this.healMoveName = "Took Deep Breaths.";
        this.damageMoveName = "Hyperventilated!";

        originalSpecialDamage = this.specialMoveDamage;
    }

    protected override void DecideIfSpecialMove()
    {
        if (this.Health > (this.MaxHealth / 3))
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                specialMoveChosen = false;
            }
            else
            {
                specialMoveChosen = true;
            }
        }
        else
        {
            specialMoveChosen = false;
        }
    }

    protected override void AttackMoveTree()
    {
        base.AttackMoveTree();
    }

    protected override void HealMoveTree()
    {
        base.HealMoveTree();
    }

    protected override void SpecialMoveSequence()
    {
        turnsPassedAfterSpecial++;

        if (turnsPassedAfterSpecial > specialMoveTurnDelay)
        {
            turnsPassedAfterSpecial = 0;
            this.specialMoveDamage = originalSpecialDamage;
            this.specialMoveName = originalSpecialName;
            this.specialMoveChosen = false;

            this.RecoverHealth(healAmountAfterCeasingToPanic);
            CommandCreator.God.ExecuteSkipTurn(this.gameObject, specialMoveFinish);
        }
        else
        {
            if (turnsPassedAfterSpecial > 1) { this.specialMoveDamage += panicDamageIncreaseRate; this.specialMoveName = specialMoveIncrementName; }
            this.ExecuteSpecialMove(false);
        }
    }
}
