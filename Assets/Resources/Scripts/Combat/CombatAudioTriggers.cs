using UnityEngine;
using System.Collections;
using Observer;

public class CombatAudioTriggers : MonoBehaviour, IObserver
{
    private CombatManager combatManager;

    private void Awake()
    {
        combatManager = GameObject.FindObjectOfType<CombatManager>();
        combatManager.Attach(this);
    }

    public void ObserverUpdate(object sender, object message)
    {
        if (sender is CombatManager)
        {
            if(message is BattleState)
            {
                switch ((BattleState)message)
                {
                    case BattleState.BattleBegins:
                        if (combatManager.participants.Find(item => item is MiniBoss) != null)
                        {
                            MusicManager.Instance.ChangeMusicState(MusicState.MiniBoss);
                        }
                        else if (combatManager.participants.Find(item => item is FinalBoss) != null)
                        {
                            MusicManager.Instance.ChangeMusicState(MusicState.FinalBoss);
                        }
                        break;
                    case BattleState.BattleEnds:
                        MusicManager.Instance.ChangeMusicState(MusicState.Main);
                        break;
                }

            }
        }
    }
}