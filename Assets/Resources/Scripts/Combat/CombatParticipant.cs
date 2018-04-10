using UnityEngine;
using System.Collections;
using Observer;
using System.Collections.Generic;
using System;

public abstract class CombatParticipant : MonoBehaviour, IDamageable, IObserver
{
    protected string commanderName = "Participant";
    public string CommanderName { get { return commanderName; } }


    public enum CombatState { Nothing, Attacking, ItemUsed, Dead }
    [HideInInspector] public CombatState combatState;

    [SerializeField] protected float health = 10f; //show in UnityEditor
    public float Health{ get { return health;  }  }
    public float MaxHealth { get; private set; }

    [SerializeField] protected float attackDamage = 2f; //show in UnityEditor
    public float AttackDamage { get { return attackDamage; } }

    protected CombatParticipant target;
    public CombatParticipant Target { get { return target; } }

    private float skipTurnTimer = 0;
    private float skipTurnDelay = 2f;
    private bool startSkipTurnTimer = false;

    private string skipTurnName = "Can't Move!";

    #region Unity Functions
    void Awake()
    {
        OnAwake();
    }

    void Update()
    {
        OnUpdate();
    }
    #endregion

    protected virtual void OnAwake()
    {
        MaxHealth = health;
    }

    protected virtual void OnUpdate()
    {
        if (startSkipTurnTimer)
        {
            skipTurnTimer += Time.deltaTime;
            if (skipTurnTimer > skipTurnDelay)
            {
                startSkipTurnTimer = false;
                skipTurnTimer = 0;
                CommandCreator.God.ExecuteSkipTurn(this.gameObject, "Can't Move!");
                //Debug.Log(this.gameObject.name);
            }
        }
    }

    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    public virtual void RecoverHealth(float recoveryAmount)
    {
        health += recoveryAmount;
    }

    public virtual void DealDamage(float amount)
    {
        target.TakeDamage(amount);
    }

    public void SetTarget(CombatParticipant newTarget)
    {
        target = newTarget;
    }

    public virtual void LoseBattle()
    {
        
    }

    public void SkipTurn(string name)
    {
        startSkipTurnTimer = true;
        skipTurnName = name;
    }

    public virtual void ObserverUpdate(object sender, object message)
    {
        if (sender is CombatManager)
        {
            CombatManager manager = (CombatManager)sender;

            if (message is BattleState)
            {
                switch ((BattleState)message)
                {
                    //for animations perhaps? I planned on this being much more useful,
                    //but then I realized the CombatManager handles everything pretty well on its own
                    //but I want to keep it in here

                    case BattleState.NextTurn:
                        OnStartNextTurn();
                        break;
                    case BattleState.TurnOver:
                        OnPerformedTurn(manager.currentParticipant);
                        break;
                    case BattleState.BattleBegins:
                        OnBeginsBattle();
                        break;
                    case BattleState.BattleEnds:
                        OnEndsBattle();
                        break;
                }
            }
        }
    }

    public virtual void OnStartNextTurn()
    {

    }
    public virtual void OnBeginsBattle()
    {

    }
    public virtual void OnEndsBattle()
    {

    }
    public virtual void OnPerformedTurn(CombatParticipant currentParticipant)
    {

    }
}
