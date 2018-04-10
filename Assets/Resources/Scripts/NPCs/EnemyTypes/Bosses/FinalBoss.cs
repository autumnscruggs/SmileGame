using UnityEngine;
using System.Collections;

public abstract class FinalBoss : NPC
{
    protected string firstSpecialMoveName;
    protected string secondSpecialMoveName;

    public FinalBoss()
    {
        this.commanderName = "The Doctor";
        firstSpecialMoveName = "First Special";
        secondSpecialMoveName = "Second Special";
    }

    public virtual void FirstSpecialMove() { }
    public virtual void SecondSpecialMove() { }

    protected void ExecuteFirstSpecial() { CommandCreator.God.ExecuteFinalBossFirstSpecial(this.gameObject, firstSpecialMoveName);}
    protected void ExecuteFirstSpecial(string addToCommandName) { CommandCreator.God.ExecuteFinalBossFirstSpecial(this.gameObject, firstSpecialMoveName + addToCommandName); }

    protected void ExecuteSecondSpecial(){  CommandCreator.God.ExecuteFinalBossSecondSpecial(this.gameObject, secondSpecialMoveName); }
    protected void ExecuteSecondSpecial(string addToCommandName) { CommandCreator.God.ExecuteFinalBossSecondSpecial(this.gameObject, secondSpecialMoveName + addToCommandName); }

    protected virtual void AttackMoveTree() { }
    protected virtual void HealMoveTree() {  }

    protected void ExecuteSkipTurn(string skip) { CommandCreator.God.ExecuteSkipTurn(this.gameObject, skip); }


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
