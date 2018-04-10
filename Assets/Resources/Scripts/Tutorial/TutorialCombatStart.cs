using UnityEngine;
using System.Collections;
using Commands;

public class TutorialCombatStart : MonoBehaviour
{
    private CombatTutorial combatTut;
    private PlayerInventory inventory;
    private TutorialText tutorial;

    void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>();
        tutorial = GameObject.FindObjectOfType<TutorialText>();
        combatTut = GameObject.FindObjectOfType<CombatTutorial>();
    }

    public void BeginCombat(GameObject objectToActOn)
    {
        CommandCreator.God.ExecuteBeginCombat(objectToActOn);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CombatParticipant participant = collider.GetComponent<CombatParticipant>();
        if (participant != null && participant != this.gameObject.GetComponent<CombatParticipant>() && !collider.isTrigger)
        {
            if (inventory.transform.GetChild(0).GetComponent<Potion>() != null)
            {
                this.gameObject.GetComponent<CombatParticipant>().SetTarget(participant);
                participant.SetTarget(this.gameObject.GetComponent<CombatParticipant>());
                BeginCombat(this.gameObject);
                combatTut.BeginCombatTutorial();
            }
            else
            {
                tutorial.SetTutorialText("You need a potion first! It's always best to have items before entering battle! Curing sadness isn't an easy task, you know!", 4f);
            }

        }
    }


}
