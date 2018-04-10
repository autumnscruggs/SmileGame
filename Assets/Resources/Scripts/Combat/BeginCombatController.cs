using UnityEngine;
using System.Collections;
using Commands;

public class BeginCombatController : MonoBehaviour
{
    private CommandProcessor commandProcessor;
    private CombatManager combatManager;

    void Awake()
    {
        commandProcessor = GameObject.FindObjectOfType<CommandProcessor>();
        combatManager = GameObject.FindObjectOfType<CombatManager>();
    }

    public void BeginCombat(GameObject objectToActOn)
    {
        CommandCreator.God.ExecuteBeginCombat(objectToActOn);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CombatParticipant participant = collider.GetComponent<CombatParticipant>();
        if (participant != null && participant.combatState != CombatParticipant.CombatState.Dead && !collider.isTrigger)
        {
            this.gameObject.GetComponent<CombatParticipant>().SetTarget(participant);
            participant.SetTarget(this.gameObject.GetComponent<CombatParticipant>());
            BeginCombat(this.gameObject);
        }
    }
}
