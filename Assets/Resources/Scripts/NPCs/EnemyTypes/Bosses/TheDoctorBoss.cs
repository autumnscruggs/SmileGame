using UnityEngine;
using System.Collections;

public class TheDoctorBoss : FinalBoss
{
    private CombatManager manager;
    public bool attackPoisonInProgress = false;
    private bool injectionInProgress = false;
    public float poisonAmount = 2f;

    private string poisonAddition = " (Poison Is Active)";
    private string injectedDamageName = "Used Injection";

    private int turnsPassedAfterInjection = 0;
    private int injectionTurns = 3;

    private bool restrainWasTheLastMove = false;

    private bool finalSpecialMoveUsed = false;

    public TheDoctorBoss()
    {
        this.damageMoveName = "Used Poison Injection.";
        this.healMoveName = "Used First Aid.";
        this.firstSpecialMoveName = "Used Restrain";
        this.secondSpecialMoveName = "Doctor's Duty";
    }

    protected override void OnAwake()
    {
        manager = GameObject.FindObjectOfType<CombatManager>();
        base.OnAwake();
    }

    public override void FirstSpecialMove()
    {
        restrainWasTheLastMove = true;
        manager.playerCanMove = false;
    }

    public override void SecondSpecialMove()
    {
        this.health += (this.MaxHealth / 2.5f);
        finalSpecialMoveUsed = true;
    }

    protected override void AttackMoveTree()
    {
        int randomAction = Random.Range(0, 2);
        switch (randomAction)
        {
            case 0:
                if (!injectionInProgress) { CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName); }
                else{ CommandCreator.God.ExecuteNPCAttack(this.gameObject, injectedDamageName); }
                break;
            case 1:
                if (!restrainWasTheLastMove)
                {
                    int rnd = Random.Range(0, 2);
                    switch (rnd)
                    {
                        case 0:
                            if (injectionInProgress) { this.ExecuteFirstSpecial(poisonAddition); }
                            else { this.ExecuteFirstSpecial(); }
                            break;
                        case 1:
                            if (injectionInProgress) { this.ExecuteSkipTurn(" Misdiagnosed!" + poisonAddition); }
                            else { this.ExecuteSkipTurn(" Misdiagnosed!"); }
                            break;
                    }
                }
                else
                {
                    restrainWasTheLastMove = false;
                    if (!injectionInProgress) { CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName); }
                    else { CommandCreator.God.ExecuteNPCAttack(this.gameObject, injectedDamageName); }
                }
                break;
        }
    }

    public override void DamageMove()
    {
        if(!injectionInProgress) { injectionInProgress = true; base.DamageMove(); }
    }

    protected override void HealMoveTree()
    {
        string fullCommandName = healMoveName;
        if (injectionInProgress) { fullCommandName += poisonAddition; }

        int randomAction = Random.Range(0, 2);
        switch (randomAction)
        {
            case 0:
                CommandCreator.God.ExecuteNPCHeal(this.gameObject, fullCommandName);
                break;
            case 1:
                if (!restrainWasTheLastMove)
                {
                    if (injectionInProgress) { this.ExecuteFirstSpecial(poisonAddition); }
                    else { this.ExecuteFirstSpecial(); }
                }
                else
                {
                    restrainWasTheLastMove = false;
                    CommandCreator.God.ExecuteNPCHeal(this.gameObject, fullCommandName);
                }
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
            int specialMoveChance = Random.Range(0, 100);
            if(randomAction <= 30 && !finalSpecialMoveUsed) //30% chance to execute second special
            {
                this.ExecuteSecondSpecial();
            }
            else
            {
                HealMoveTree();
            }
        }
    }

    public void Poison()
    {
        this.DealDamage(poisonAmount);
    }

    public override void OnPerformedTurn(CombatParticipant currentParticipant)
    {
        if(currentParticipant == this)
        {
            manager.playerCanMove = true;

            if (injectionInProgress)
            {
                turnsPassedAfterInjection++;
                if (turnsPassedAfterInjection >= injectionTurns)
                {
                    turnsPassedAfterInjection = 0;
                    injectionInProgress = false;
                }
                else
                {
                    Poison();
                }
            }
        }
        base.OnPerformedTurn(currentParticipant);
    }
}

