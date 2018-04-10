using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private CombatManager combatManager;
    private bool finalBossFight = false;

	void Awake ()
    {
        combatManager = GameObject.FindObjectOfType<CombatManager>();
    }
	
	void Update ()
    {
	    if(!finalBossFight && combatManager.combatIsHappening && combatManager.participants.Find(item => item is FinalBoss) != null)
        {
            finalBossFight = true;
        }

        if (finalBossFight && !combatManager.combatIsHappening && combatManager.deadParticipant is NPC)
        {
            EndSlateManager.Instance.Victory();
        }
	}
}
