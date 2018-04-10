using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Observer;

public enum Turn { Player, NPC }
public enum BattleState { NextTurn, DoingTurn, TurnOver, BattleBegins, Victory, BattleEnds}

public class CombatManager : MonoBehaviour, ISubject
{
    [HideInInspector] public bool combatIsHappening;

    public List<IObserver> observers;
    [HideInInspector] public List<string> observerNames; //for debugging which observers are attached in inspector
    [HideInInspector] public List<CombatParticipant> participants;
    private int turnIndicator;
    public CombatParticipant currentParticipant;

    public Turn turn;
    public BattleState state;

    [HideInInspector] public CombatParticipant deadParticipant = null;
    public float endBattleDelay = 2f;
    private float endBattleTimer = 0;
    private bool startEndBattleTimer = false;

    [HideInInspector] public bool playerCanMove = true;

    public static bool enemyCanPerformTurn = true; //you don't have to tell me how bad this is

    public CombatManager()
    {
        observerNames = new List<string>();
        observers = new List<IObserver>();
        participants = new List<CombatParticipant>();
        turnIndicator = 0;
    }

    void Update()
    {
        EndBattleTimer();
        if (combatIsHappening) { CheckForBattleEnding(); }
    }

    private void EndBattleTimer()
    {
        if (startEndBattleTimer)
        {
            endBattleTimer += Time.deltaTime;
            if (endBattleTimer > endBattleDelay)
            {
                CommandCreator.God.ExecuteEndCombat(deadParticipant.gameObject);
                EndBattle();
                startEndBattleTimer = false;
                endBattleTimer = 0;
            }
        }
    }

    private void CheckForBattleEnding()
    {
        if (deadParticipant != null)
        {
            enemyCanPerformTurn = false;
            CommandCreator.God.ExecuteWin(this.gameObject);
        }
        else
        {
            enemyCanPerformTurn = true;
            deadParticipant = participants.Find(item => item.Health <= 0);
        }
    }

    public void StartEndBattleTimer()
    {
        Notify(BattleState.Victory);
        startEndBattleTimer = true;
    }

    public void PerformTurn()
    {
        if (!startEndBattleTimer)
        {
            NextTurn();
            Notify(BattleState.TurnOver);
        }
    }

    public void NextTurn()
    {
        IncrementTurn();
        Notify(BattleState.NextTurn);

        if (currentParticipant is Player)
        {
            if (playerCanMove) { Notify(Turn.Player); }
            else { currentParticipant.GetComponent<Player>().SkipTurn("Can't Move!"); }
        }
        else if (currentParticipant is NPC) { Notify(Turn.NPC); }
    }

    private void IncrementTurn()
    {
        turnIndicator++;
        if (turnIndicator >= (participants.Count))
        { turnIndicator = 0; }
        currentParticipant = participants[turnIndicator];
    }

    public void BeginBattle()
    {
        deadParticipant = null;
        combatIsHappening = true;
        turnIndicator = 0;
        currentParticipant = participants[turnIndicator];
        Notify(BattleState.BattleBegins);
        PauseMenu.PauseGame(true);
    }

    public void EndBattle()
    {
        combatIsHappening = false;
        turnIndicator = 0;
        Notify(BattleState.BattleEnds);

        //Clear observers & participants cuz battle is over
        foreach (CombatParticipant cP in participants) { if (observers.Contains(cP)) { Detach(cP); } }
        participants.Clear();

        PauseMenu.PauseGame(false);
    }

    public bool ContainsObserver(IObserver o)
    {
        return observers.Contains(o);
    }

    public void ResetTurns()
    {
        NextTurn();
    }

    public void Attach(IObserver o)
    {
        MonoBehaviour monoBehavior = (MonoBehaviour)o;
        observerNames.Add(monoBehavior.GetType().Name);
        observers.Add(o);
        if (o is CombatParticipant) { participants.Add((CombatParticipant) o); }
    }

    public void Detach(IObserver o)
    {
        MonoBehaviour monoBehavior = (MonoBehaviour)o;
        observerNames.Remove(monoBehavior.GetType().Name);
        observers.Remove(o);
        //if (o is CombatParticipant) { participants.Remove((CombatParticipant)o); }
    }

    public void Notify(object s)
    {
        //Debug.Log("Notifying observers of.... " + s);
        if (observers.Count > 0)
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, s);
            }
        }
    }

    public void Notify(BattleState s)
    {
        //Debug.Log("Notifying observers of.... " + s);

        state = s;

        if (observers.Count > 0)
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, s);
            }
        }
    }

    public void Notify(Turn s)
    {
        //Debug.Log("Notifying observers of.... " + s);

        turn = s;

        if (observers.Count > 0)
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, s);
            }
        }
    }
}
