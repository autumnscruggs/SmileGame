using UnityEngine;
using System.Collections;

public abstract class MiniBoss : NPC
{
    protected string specialMoveName;
    public float specialMoveDamage;
    protected bool specialMoveChosen;

    public MiniBoss()
    {
        specialMoveName = "Special Move!";
    }

    public virtual void SpecialMove()
    {
        this.DealDamage(specialMoveDamage);
    }

    protected void ExecuteSpecialMove(bool doneWithSpecial)
    {
        CommandCreator.God.ExecuteMiniBossSpecial(this.gameObject, specialMoveName);
        if (doneWithSpecial){ specialMoveChosen = false; }
    }

    protected virtual void AttackMoveTree()
    {
        CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
    }

    protected virtual void HealMoveTree()
    {
        CommandCreator.God.ExecuteNPCHeal(this.gameObject, healMoveName);
    }

    protected virtual void SpecialMoveSequence()
    {
        ExecuteSpecialMove(true);
    }

    protected override void FullAIDecisionTree()
    {
        if (!specialMoveChosen) { DecideIfSpecialMove(); }

        if (!specialMoveChosen)
        {
            base.FullAIDecisionTree();
        }
        else
        {
            SpecialMoveSequence();
        }

    }

    protected virtual void DecideIfSpecialMove()
    {
        if(this.Health < (this.MaxHealth / 2))
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
    protected override void AboveHalfHealthActions()
    {
        AttackMoveTree();
    }
    protected override void BetweenHalfHealthAndQuarterActions()
    {
        int randomAction = Random.Range(0, 2);
        switch (randomAction)
        {
            case 0:
                AttackMoveTree();
                break;
            case 1:
                HealMoveTree();
                break;
        }

    }
    protected override void LessThanQuarterHealthActions()
    {
        int randomAction = Random.Range(0, 4);
        if (randomAction == 0)
        {
            AttackMoveTree();
        }
        else
        {
            HealMoveTree();
        }
    }

}
