using UnityEngine;
using System.Collections;

public enum NPCState { Idle, Walking, InCombat }

public abstract class NPC : CombatParticipant
{
    private CombatManager manager;

    private NPCController npcController;
    public NPCState npcState;
    public float actionDelay = 2f;
    [SerializeField] private float actionTimer = 0f;
    private bool startActionTimer = false;

    private bool wentInActionTimer = false;
    private float antiFreeze = 0;

    public float healAmountWhenCry = 2f;

    private SpriteRenderer renderer;
    public Sprite loseSprite;

    protected string damageMoveName;
    protected string healMoveName;

    protected override void OnAwake()
    {
        base.OnAwake();
        manager = GameObject.FindObjectOfType<CombatManager>();
        renderer = this.GetComponent<SpriteRenderer>();
        npcController = this.GetComponent<NPCController>();
    }


    public NPC()
    {
        this.commanderName = "NPC";
        damageMoveName = "Attack!";
        healMoveName = "Heal!";
    }

    void Update()
    {
        if (startActionTimer)
        {
            actionTimer += Time.deltaTime;
            if (actionTimer > actionDelay)
            {
                //Debug.Log("Performing Action");
                ExecuteActionCommand();
                startActionTimer = false;
                wentInActionTimer = true;
                actionTimer = 0;
            }
        }
    }


    public override void LoseBattle()
    {
        renderer.sprite = loseSprite;
        this.combatState = CombatState.Dead;
        if (npcController != null) { npcController.enabled = false; }
        base.LoseBattle();
    }

    public virtual void DamageMove()
    {
        this.DealDamage(attackDamage);
    }
    public virtual void HealMove()
    {
        this.RecoverHealth(healAmountWhenCry);
    }

    protected void PerformAction()
    {
       startActionTimer = true;
    }
    protected void ExecuteActionCommand()
    {
        if (CombatManager.enemyCanPerformTurn)
        {
            FullAIDecisionTree();
        }
    }

    protected virtual void AboveHalfHealthActions()
    {
        CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
    }

    protected virtual void BetweenHalfHealthAndQuarterActions()
    {
        int randomAction = Random.Range(0, 2);
        switch (randomAction)
        {
            case 0:
                CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
                break;
            case 1:
                CommandCreator.God.ExecuteNPCHeal(this.gameObject, healMoveName);
                break;
        }
    }

    protected virtual void LessThanQuarterHealthActions()
    {
        int randomAction = Random.Range(0, 4);
        if (randomAction == 0)
        {
            CommandCreator.God.ExecuteNPCAttack(this.gameObject, damageMoveName);
        }
        else
        {
            CommandCreator.God.ExecuteNPCHeal(this.gameObject, healMoveName);
        }
    }

    protected virtual void NearlyDeadActions()
    {
        CommandCreator.God.ExecuteNPCHeal(this.gameObject, healMoveName);
    }

    protected virtual void FullAIDecisionTree() //"AI"
    {
        if (this.health > (this.MaxHealth / 2)) //when above half health - only attack 
        {
            AboveHalfHealthActions();
        }
        else if (this.health > (this.MaxHealth / 4) && this.health <= (this.MaxHealth / 2)) //when less than half health - 1/2 to heal
        {
            BetweenHalfHealthAndQuarterActions();
        }
        else if(this.health <= (this.MaxHealth / 4) && this.health > (this.MaxHealth / 6)) //when less than a quarter health - 3/4 chance to heal
        {
            LessThanQuarterHealthActions();
        }
        else if(this.health < (this.MaxHealth / 6)) //guaranteed heal by this point
        {
            NearlyDeadActions();
        }
        
    }

    public override void ObserverUpdate(object sender, object message)
    {
        if (sender is CombatManager)
        {
            if (message is Turn && (Turn)message == Turn.NPC)
            {
                wentInActionTimer = false;
                PerformAction();
            }
        }

        base.ObserverUpdate(sender, message);
    }
}
