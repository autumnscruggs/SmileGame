using UnityEngine;
using System.Collections;
using Observer;
using System;

public class CombatVictoryManager : MonoBehaviour, IObserver
{
    private CombatManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<CombatManager>();
        manager.Attach(this);
    }

    public void ObserverUpdate(object sender, object message)
    {
        if(sender is CombatManager)
        {
            if(message is BattleState)
            {
                switch ((BattleState)message)
                {
                    case BattleState.Victory:
                        if (manager.currentParticipant is NPC)
                        {
                            NPC npc = (NPC)manager.participants.Find(item => item is NPC);
                            npc.LoseBattle();
                        }
                        else
                        {
                            Player player = (Player)manager.participants.Find(item => item is Player);
                            player.LoseBattle();
                        }
                        break;
                }
            }
        }   
    }
}


