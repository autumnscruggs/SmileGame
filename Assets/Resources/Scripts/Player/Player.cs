using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Observer;

public enum PlayerState { Idle, Walking, PickupItem, InCombat }

public class Player : CombatParticipant
{
    private PlayerController controller;
    private PlayerInventory inventory;

    public PlayerState PState
    {
        get { return playerState; }
        set
        {
            playerState = value;
            StateChange();
        }
    }
    [SerializeField] private PlayerState playerState;

    public float complimentDamageValue;
    public float complimentRecoilValue;

    public Player()
    {
        this.commanderName = "Player";
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        playerState = PlayerState.Idle;
        controller = this.GetComponent<PlayerController>();
    }

    public void StateChange()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                controller.enabled = true;
                break;
            case PlayerState.InCombat:
                controller.enabled = false;
                break;
            case PlayerState.PickupItem:
                controller.enabled = true;
                break;
            case PlayerState.Walking:
                controller.enabled = true;
                break;
        }
    }

    public override void LoseBattle()
    {
        EndSlateManager.Instance.GameOver();
        base.LoseBattle();
    }

    public override void OnBeginsBattle()
    {
        base.OnBeginsBattle();
        playerState = PlayerState.InCombat;
    }

    public override void OnEndsBattle()
    {
        base.OnEndsBattle();
        playerState = PlayerState.Idle;
    }

    //--Combat Moves--//

    public void Smile()
    {
        this.DealDamage(attackDamage);
        System.Threading.Thread.Sleep(10);
    }

    public void Compliment()
    {
        this.TakeDamage(complimentRecoilValue);
        this.DealDamage(complimentDamageValue);
    }
}
