using UnityEngine;
using System.Collections;
using Commands;

public class EndCombatController : MonoBehaviour
{
    private CommandProcessor commandProcessor;

    void Awake()
    {
        commandProcessor = GameObject.FindObjectOfType<CommandProcessor>();
    }

    public void EndCombat(GameObject objectToActOn)
    {
        EndBattleCommand command = new EndBattleCommand();
        commandProcessor.ExecuteCommand(command, objectToActOn);
    }
   
}
